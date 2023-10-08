using Descriptors;
using PlayerLogic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI
{
    [RequireComponent(typeof(Slider))]
    public class PlayerHealthBar : MonoBehaviour
    {
        [SerializeField]
        private Player _player;
        
        [Inject]
        private PlayerDescriptor _playerDescriptor;
        
        private Slider _slider;

        private void Awake()
        {
            _slider = GetComponent<Slider>();
        }

        private void OnEnable()
        {
            _player.OnDamageReceived += UpdateHealthBar;
        }

        private void OnDisable()
        {
            _player.OnDamageReceived -= UpdateHealthBar;
        }

        private void UpdateHealthBar()
        {
            _slider.value = _player.CurrentHp / _playerDescriptor.Hp;
        }
    }
}
