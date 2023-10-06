using UnityEngine;

namespace EnemyLogic
{
    public class Enemy : MonoBehaviour
    {
        public float HP { get; private set; } = 100;

        public void TakeDamage(float damage)
        {
            HP -= damage;
            
            if (HP <= 0)
            {
                Die();
            }
        }

        private void Die()
        {
            Destroy(gameObject);
        }
    }
}