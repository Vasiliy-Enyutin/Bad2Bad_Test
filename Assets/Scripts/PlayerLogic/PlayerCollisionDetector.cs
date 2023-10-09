using Inventory;
using UnityEngine;

namespace PlayerLogic
{
    [RequireComponent(typeof(InventoryController))]
    public class PlayerCollisionDetector : MonoBehaviour
    {
        private InventoryController _inventoryController;
        
        private void Awake()
        {
            _inventoryController = GetComponent<InventoryController>();
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.TryGetComponent(out GameItem gameItem))
            {
                _inventoryController.AddItem(gameItem.GameItemInfo);
                Destroy(gameItem.gameObject);
            }
        }
    }
}
