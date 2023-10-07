using Pathfinding;
using UnityEngine;

namespace EnemyLogic
{
    [RequireComponent(typeof(AIPath))]
    public class EnemyRotator : MonoBehaviour
    {
        [SerializeField]
        private Transform _enemyGfxTransform;
        
        private AIPath _aiPath = null!;

        private void Awake()
        {
            _aiPath = GetComponent<AIPath>();
        }

        private void Update()
        {
            if (_aiPath.velocity.x > 0)
            {
                _enemyGfxTransform.localScale = new Vector3(1f, 1f, 1f);
            }
            else if (_aiPath.velocity.x < 0)
            {
                _enemyGfxTransform.localScale = new Vector3(-1f, 1f, 1f);
            }
        }
    }
}
