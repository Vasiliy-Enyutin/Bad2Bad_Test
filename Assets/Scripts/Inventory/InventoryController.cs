using UnityEngine;

namespace Inventory
{
    public class InventoryController : MonoBehaviour
    {
        private readonly InventoryModel _inventoryModel = new();

        private InventoryView _inventoryView;

        private void OnEnable()
        {
            _inventoryModel.OnItemAdded += AddNewItemView;
            _inventoryModel.OnItemQuantityIncreased += IncreaseItemQuantityView;
        }

        private void OnDisable()
        {
            _inventoryModel.OnItemAdded -= AddNewItemView;
            _inventoryModel.OnItemQuantityIncreased -= IncreaseItemQuantityView;
        }

        private void Start()
        {
            _inventoryView = FindObjectOfType<InventoryView>();
        }

        public void AddItem(GameItem gameItem)
        {
            _inventoryModel.AddItem(gameItem);
        }

        private void AddNewItemView(GameItem gameItem)
        {
            _inventoryView.AddItem(gameItem);
        }

        private void IncreaseItemQuantityView(int itemId, int quantity)
        {
            _inventoryView.IncreaseItemQuantity(itemId, quantity);
        }
    }
}