using Pathfinding;
using Services;
using UnityEngine;
using Zenject;

namespace EnemyLogic
{
    [RequireComponent(typeof(EnemyCollisionDetector))]
    [RequireComponent(typeof(AIDestinationSetter))]
    [RequireComponent(typeof(AIPath))]
    public class EnemyPathfinderController : MonoBehaviour
    {
        [Inject]
        private GameFactoryService _gameFactoryService;
        
        private EnemyCollisionDetector _enemyCollisionDetector;
        private AIDestinationSetter _destinationSetter = null!;
        private AIPath _aiPath;

        private void Awake()
        {
            _enemyCollisionDetector = GetComponent<EnemyCollisionDetector>();
            _destinationSetter = GetComponent<AIDestinationSetter>();
            _destinationSetter.target = _gameFactoryService.Player.transform;
            _aiPath = GetComponent<AIPath>();
            _aiPath.enabled = false;
        }

        private void OnEnable()
        {
            _enemyCollisionDetector.OnPlayerDetected += EnablePathfinder;
            _enemyCollisionDetector.OnLostPlayer += DisablePathfinder;
        }

        private void OnDisable()
        {
            _enemyCollisionDetector.OnPlayerDetected -= EnablePathfinder;
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