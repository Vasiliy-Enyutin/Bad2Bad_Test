using UnityEngine;

namespace Descriptors
{
    [CreateAssetMenu(fileName = "PlayerLocationDescriptor", menuName = "Descriptors/PlayerLocation", order = 0)]
    public class PlayerLocationDescriptor : ScriptableObject
    {
        public Vector3 InitialPlayerPositionPoint;
    }
}
