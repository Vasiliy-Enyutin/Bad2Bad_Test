using System.Collections;
using Descriptors;
using EnemyLogic;
using Services;
using UnityEngine;
using Zenject;

namespace PlayerLogic
{
    public class ShootingController : MonoBehaviour
    {
        [SerializeField]
        private Transform _firePoint;
        [SerializeField]
        private GameObject _impactEffect;
        [SerializeField]
        private LineRenderer _tracerBulletEffect;

        [Inject]
        private AssetProviderService _assetProvider;
        [Inject]
        private PlayerInputService _playerInputService;
        [Inject]
        private PlayerDescriptor _playerDescriptor;

        private const float MAX_SHOOTING_DISTANCE = 10;
        
        private PlayerMovement _playerMovement;

        private float _shotTimer = 0f;

        private void Awake()
        {
            _playerMovement = GetComponent<PlayerMovement>();
        }

        private void Update()
        {
            _shotTimer += Time.deltaTime;

            if (_playerInputService.IsFireButtonDown && _shotTimer >= _playerDescriptor.TimeBetweenShots)
            {
                StartCoroutine(Shoot());
                _shotTimer = 0f;
            }
        }

        private IEnumerator Shoot()
        {
            Vector3 shootDirection = _playerMovement.FacingRight ? _firePoint.right : -_firePoint.right;
            RaycastHit2D hit = Physics2D.Raycast(_firePoint.position, shootDirection);
            Debug.Log(hit.point);

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
                _tracerBulletEffect.SetPosition(1, _firePoint.position + shootDirection * MAX_SHOOTING_DISTANCE);
                AddImpactEffect(_firePoint.position + shootDirection * MAX_SHOOTING_DISTANCE);
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
        
        // private bool TryGetEnemy(RaycastHit2D hit, out Enemy enemy)
        // {
        //     enemy = null;
        //     float currentDistance = 0f;
        //
        //     while (hit && currentDistance < MAX_SHOOTING_DISTANCE)
        //     {
        //         Debug.Log(hit.collider.isTrigger);
        //         if (!hit.collider.isTrigger)
        //         {
        //             Debug.Log("NOT TRIGGET");
        //             enemy = hit.collider.GetComponent<Enemy>();
        //             if (enemy != null)
        //             {
        //                 return true;
        //             }
        //         }
        //
        //         currentDistance += hit.distance;
        //
        //         // Если попали в триггер коллайдер, продолжаем луч дальше
        //         hit = Physics2D.Raycast(hit.point + hit.normal * 0.01f, hit.point + hit.normal * MAX_SHOOTING_DISTANCE);
        //     }
        //
        //     return false;
        // }

        private void AddImpactEffect(Vector3 position)
        {
            Instantiate(_impactEffect, position, Quaternion.identity);
        }
    }
}