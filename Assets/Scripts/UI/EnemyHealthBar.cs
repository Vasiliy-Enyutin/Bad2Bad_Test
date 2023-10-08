using Descriptors;
using EnemyLogic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI
{
    public class EnemyHealthBar : MonoBehaviour
    {
        [SerializeField]
        private Enemy _enemy;
        
        [Inject]
        private EnemyDescriptor _enemyDescriptor;
        
        private Slider _slider;

        private void Awake()
        {
            _slider = GetComponent<Slider>();
        }

        private void OnEnable()
        {
            _enemy.OnDamageReceived += UpdateHealthBar;
        }

        private void OnDisable()
        {
            _enemy.OnDamageReceived -= UpdateHealthBar;
        }

        private void UpdateHealthBar()
        {
            _slider.value = _enemy.CurrentHp / _enemyDescriptor.Hp;
        }
    }
}
