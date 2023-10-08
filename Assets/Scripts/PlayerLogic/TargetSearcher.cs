using System.Linq;
using Descriptors;
using EnemyLogic;
using UnityEngine;
using Zenject;

namespace PlayerLogic
{
    public class TargetSearcher : MonoBehaviour
    {
        [Inject]
        private PlayerDescriptor _playerDescriptor = null!;
        
        public Enemy TargetEnemy { get; private set; }

        private void Update()
        {
            UpdateTargetEnemy();
        }
        
        private void UpdateTargetEnemy()
        {
            // Получаем все коллайдеры в радиусе, затем фильтруем только обычные коллайдеры
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, _playerDescriptor.ShootTargetRadius)
                .Where(collider => !collider.isTrigger)
                .ToArray();
            
            if (colliders.Length > 0)
            {
                float closestDistance = float.MaxValue;
                Enemy closestEnemy = null;

                foreach (Collider2D collider in colliders)
                {
                    if (collider.TryGetComponent(out Enemy enemy))
                    {
                        if (enemy == TargetEnemy)
                        {
                            return;
                        }
                        
                        float distance = Vector2.Distance(transform.position, enemy.transform.position);
                        if (distance < closestDistance)
                        {
                            closestDistance = distance;
                            closestEnemy = enemy;
                        }
                    }
                }

                TargetEnemy = closestEnemy == null ? null : closestEnemy;
            }
            else
            {
                TargetEnemy = null;
            }
        }
    }
}
