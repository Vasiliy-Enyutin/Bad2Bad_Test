using PlayerLogic;
using Services;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class ServiceInstaller : MonoInstaller
    {
        [SerializeField]
        private PlayerInputService _playerInputServicePrefab = null!;

        public override void InstallBindings()
        {
            Container.Bind<AssetProviderService>().AsSingle();
            Container.Bind<PlayerInputService>().FromComponentInNewPrefab(_playerInputServicePrefab).AsSingle();
            Container.Bind<GameFactoryService>().AsSingle();
        }
    }
}