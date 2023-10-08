using System;
using PlayerLogic;
using UnityEngine;

namespace EnemyLogic
{
    public class EnemyCollisionDetector : MonoBehaviour
    {
        public event Action OnPlayerDetected;
        public event Action OnLostPlayer;

        public event Action OnPlayerCollisionEnter;
        public event Action OnPlayerCollisionExit;
        
        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.TryGetComponent(out Player _))
            {
                OnPlayerDetected?.Invoke();
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.TryGetComponent(out Player _))
            {
                OnLostPlayer?.Invoke();
            }
        }

        private void OnCollisionEnter2D(Collision2D col)
        {
            if (col.collider.TryGetComponent(out Player _))
            {
                OnPlayerCollisionEnter?.Invoke();
            }
        }

        private void OnCollisionExit2D(Collision2D other)
        {
            if (other.collider.TryGetComponent(out Player _))
            {
                OnPlayerCollisionExit?.Invoke();
            }
        }
    }
}
