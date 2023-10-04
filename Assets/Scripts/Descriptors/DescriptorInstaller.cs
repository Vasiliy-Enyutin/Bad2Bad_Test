using UnityEngine;
using Zenject;

namespace Descriptors
{
    [CreateAssetMenu(fileName = "Custom Installers", menuName = "DescriptorInstaller", order = 0)]
    public class DescriptorInstaller : ScriptableObjectInstaller
    {
        [SerializeField]
        private PlayerDescriptor _playerDescriptor = null!;
        [SerializeField]
        private PlayerLocationDescriptor _playerLocationDescriptor = null!;
        // [SerializeField] 
        // private EnemyDescriptor _enemyDescriptor;
        // [SerializeField]
        // private UiDescriptor _uiDescriptor;
		
        public override void InstallBindings()
        {
            Container.BindInstance(_playerDescriptor).AsSingle();
            Container.BindInstance(_playerLocationDescriptor).AsSingle();
            // Container.BindInstance(_enemyDescriptor).AsSingle();
            // Container.BindInstance(_uiDescriptor).AsSingle();
        }
    }
}
