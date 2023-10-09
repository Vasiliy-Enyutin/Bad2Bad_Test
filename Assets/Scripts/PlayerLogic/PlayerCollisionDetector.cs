using Inventory;
using UnityEngine;

namespace PlayerLogic
{
    public class PlayerCollisionDetector : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.TryGetComponent(out LootableItem lootableItem))
            {
                Debug.Log(lootableItem.ItemName);
            }
        }
    }
}
