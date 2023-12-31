using System.Collections;
using Descriptors;
using EnemyLogic;
using Inventory;
using Services;
using UnityEngine;
using Zenject;

namespace PlayerLogic
{
    [RequireComponent(typeof(Player))]
    [RequireComponent(typeof(PlayerMovement))]
    [RequireComponent(typeof(InventoryController))]
    public class ShootingController : MonoBehaviour
    {
        [SerializeField]
        private Transform _firePoint;
        [SerializeField]
        private GameObject _impactEffect;
        [SerializeField]
        private LineRenderer _tracerBulletEffect;
        
        [Inject]
        private PlayerInputService _playerInputService = null!;

        private InventoryController _inventoryController = null!;
        private PlayerDescriptor _playerDescriptor = null!;
        private PlayerMovement _playerMovement = null!;

        private float _shotTimer = 0f;

        private void Start()
        {
            _inventoryController = GetComponent<InventoryController>();
            _playerDescriptor = GetComponent<Player>().PlayerDescriptor;
            _playerMovement = GetComponent<PlayerMovement>();
        }

        private void Update()
        {
            _shotTimer += Time.deltaTime;

            if (_playerInputService.IsFireButtonDown && _shotTimer >= _playerDescriptor.TimeBetweenShots)
            {
                if (_inventoryController.TryGetAmmo())
                {
                    StartCoroutine(Shoot());
                    _shotTimer = 0f;
                }
            }
        }

        private IEnumerator Shoot()
        {
            Vector3 shootDirection = _playerMovement.FacingRight ? _firePoint.right : -_firePoint.right;
            RaycastHit2D hit = Physics2D.Raycast(_firePoint.position, shootDirection);

            if (TryGetEnemy(hit, out Enemy enemy))
            {
                enemy.TakeDamage(_playerDescriptor.Damage);
                
                _tracerBulletEffect.SetPosition(0, _firePoint.position);
                _tracerBulletEffect.SetPosition(1, hit.point);
                AddImpactEffect(hit.point);
            }
            else
            {
                _tracerBulletEffect.SetPosition(0, _firePoint.position);
                _tracerBulletEffect.SetPosition(1, _firePoint.position + shootDirection * _playerDescriptor.MaxShootingDistance);
                AddImpactEffect(_firePoint.position + shootDirection * _playerDescriptor.MaxShootingDistance);
            }

            _tracerBulletEffect.enabled = true;
            yield return new WaitForSeconds(0.02f);
            _tracerBulletEffect.enabled = false;
        }
        
        private bool TryGetEnemy(RaycastHit2D hit, out Enemy enemy)
        {
            enemy = null;
    
            if (hit)
            {
                enemy = hit.collider.GetComponent<Enemy>();
                if (enemy != null)
                {
                    return true;
                }
            }
    
            return false;
        } 

        private void AddImpactEffect(Vector3 position)
        {
            Instantiate(_impactEffect, position, Quaternion.identity);
        }
    }
}