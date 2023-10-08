using System;
using PlayerLogic;
using UnityEngine;

namespace EnemyLogic
{
    public class EnemyCollisionDetector : MonoBehaviour
    {
        public event Action OnSawPlayer;
        public event Action OnLostPlayer;
        
        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.TryGetComponent(out Player _))
            {
                OnSawPlayer?.Invoke();
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.TryGetComponent(out Player _))
            {
                OnLostPlayer?.Invoke();
            }
        }
    }
}
