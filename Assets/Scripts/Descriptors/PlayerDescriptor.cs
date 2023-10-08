using PlayerLogic;
using UnityEngine;

namespace Descriptors
{
    [CreateAssetMenu(fileName = "PlayerDescriptor", menuName = "Descriptors/Player", order = 0)]
    public class PlayerDescriptor : ScriptableObject
    {
        public Player Prefab = null!;
        public float Hp;
        public float MoveSpeed;
        public float Damage;
        public float TimeBetweenShots;
        public float ShootTargetRadius;
        public float MaxShootingDistance;
    }
}
