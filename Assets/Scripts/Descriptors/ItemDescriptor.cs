using UnityEngine;

namespace Descriptors
{
    [CreateAssetMenu(fileName = "ItemDescriptor", menuName = "Descriptors/ItemDescriptor", order = 0)]
    public class ItemDescriptor : ScriptableObject
    {
        public int Id;
        public string ItemName;
        public int Quantity;
        public Sprite Icon;
    }
}
