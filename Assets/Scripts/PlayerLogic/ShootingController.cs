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

            if (hit)
            {
                if (hit.collider.TryGetComponent(out Enemy enemy))
                {
                    enemy.TakeDamage(_playerDescriptor.Damage);
                }

                Instantiate(_impactEffect, hit.point, Quaternion.identity);
                
                _tracerBulletEffect.SetPosition(0, _firePoint.position);
                _tracerBulletEffect.SetPosition(1, hit.point);
            }
            else
            {
                _tracerBulletEffect.SetPosition(0, _firePoint.position);
                _tracerBulletEffect.SetPosition(1, _firePoint.position + shootDirection * 10);
            }

            _tracerBulletEffect.enabled = true;
            yield return new WaitForSeconds(0.02f);
            _tracerBulletEffect.enabled = false;
        }
    }
}