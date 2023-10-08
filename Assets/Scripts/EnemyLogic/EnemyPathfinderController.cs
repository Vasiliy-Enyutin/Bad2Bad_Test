using Pathfinding;
using UnityEngine;

namespace EnemyLogic
{
    [RequireComponent(typeof(EnemyCollisionDetector))]
    [RequireComponent(typeof(AIPath))]
    public class EnemyPathfinderController : MonoBehaviour
    {
        private EnemyCollisionDetector _enemyCollisionDetector;
        private AIPath _aiPath;

        private void Awake()
        {
            _enemyCollisionDetector = GetComponent<EnemyCollisionDetector>();
            _aiPath = GetComponent<AIPath>();
            _aiPath.enabled = false;
        }

        private void OnEnable()
        {
            _enemyCollisionDetector.OnSawPlayer += EnablePathfinder;
            _enemyCollisionDetector.OnLostPlayer += DisablePathfinder;
        }

        private void OnDisable()
        {
            _enemyCollisionDetector.OnSawPlayer -= EnablePathfinder;
            _enemyCollisionDetector.OnLostPlayer -= DisablePathfinder;
        }

        private void EnablePathfinder()
        {
            _aiPath.enabled = true;
        }

        private void DisablePathfinder()
        {
            _aiPath.enabled = false;
        }
    }
}