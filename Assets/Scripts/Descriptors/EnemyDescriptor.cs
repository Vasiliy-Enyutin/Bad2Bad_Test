using EnemyLogic;
using UnityEngine;

namespace Descriptors
{
    [CreateAssetMenu(fileName = "EnemyDescriptor", menuName = "Descriptors/EnemyDescriptor", order = 0)]
    public class EnemyDescriptor : ScriptableObject
    {
        public Enemy Enemy;
        public float Hp;
        public int EnemiesNumber;
        public float MoveSpeed;
        public float PursuitDistance;
    }
}
