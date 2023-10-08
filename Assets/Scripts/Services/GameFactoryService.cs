using System.Collections.Generic;
using Descriptors;
using EnemyLogic;
using JetBrains.Annotations;
using PlayerLogic;
using UnityEngine;
using Zenject;

namespace Services
{
    [UsedImplicitly]
    public class GameFactoryService
    {
        [Inject]
        private AssetProviderService _assetProviderService = null!;
        [Inject]
        private PlayerDescriptor _playerDescriptor = null!;
        [Inject]
        private PlayerLocationDescriptor _playerLocationDescriptor = null!;
        [Inject]
        private EnemyDescriptor _enemyDescriptor = null!;
        
        public Player Player { get; private set; }
        
        public List<Enemy> Enemies { get; } = new();

        
        public void CreatePlayer()
        {
            Player = _assetProviderService.CreateAsset<Player>(_playerDescriptor.Prefab, _playerLocationDescriptor.InitialPlayerPositionPoint);
            Player.Init(_playerDescriptor);
        }

        public void CreateEnemies(List<Vector3> cellPositions)
        {
            List<Vector3> availablePositions = new List<Vector3>(cellPositions);

            for (int i = 0; i < _enemyDescriptor.EnemiesNumber; i++)
            {
                if (availablePositions.Count == 0)
                {
                    break;
                }

                int randomIndex = Random.Range(0, availablePositions.Count);
                Vector3 spawnPosition = availablePositions[randomIndex];
                availablePositions.RemoveAt(randomIndex);

                Enemy enemy = _assetProviderService.CreateAsset<Enemy>(_enemyDescriptor.Enemy, spawnPosition);
                enemy.Init(Player.transform, _enemyDescriptor.Hp);
                Enemies.Add(enemy);
            }
        }
    }
}
