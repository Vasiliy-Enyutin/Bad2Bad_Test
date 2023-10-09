using UnityEngine;

namespace Descriptors
{
    [CreateAssetMenu(fileName = "ItemsList", menuName = "Descriptors/ItemsList", order = 0)]
    public class ItemsListDescriptor : ScriptableObject
    {
        public ItemDescriptor[] Items;
    }
}
