using UnityEngine;

namespace EnemyLogic
{
    public class EnemyAnimationController : MonoBehaviour
    {
        [SerializeField]
        private Animator _animator;

        public void PlayAttackAnimation()
        {
            _animator.Play("EnemyAttack");
        }

        public void PlayIdleAnimation()
        {
            _animator.Play("EnemyIdle");
        }
    }
}
