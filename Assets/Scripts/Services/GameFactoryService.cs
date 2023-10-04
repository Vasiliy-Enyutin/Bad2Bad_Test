using Descriptors;
using JetBrains.Annotations;
using PlayerLogic;
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
        
        public Player Player { get; private set; }
        
        public void CreatePlayer()
        {
            Player = _assetProviderService.CreateAsset<Player>(_playerDescriptor.Prefab, _playerLocationDescriptor.InitialPlayerPositionPoint);
        }
    }
}
