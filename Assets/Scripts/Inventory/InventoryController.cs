using UnityEngine;

namespace Inventory
{
    public class InventoryController : MonoBehaviour
    {
        private readonly InventoryModel _inventoryModel = new();

        private InventoryView _inventoryView;

        private void Awake()
        {
            _inventoryView = FindObjectOfType<InventoryView>();
        }

        private void OnEnable()
        {
            _inventoryModel.OnItemAdded += AddNewItemView;
            _inventoryModel.OnItemQuantityIncreased += IncreaseItemQuantityView;

            _inventoryView.OnRemoveButtonClicked += RemoveItem;
        }

        private void OnDisable()
        {
            _inventoryModel.OnItemAdded -= AddNewItemView;
            _inventoryModel.OnItemQuantityIncreased -= IncreaseItemQuantityView;
            
            _inventoryView.OnRemoveButtonClicked -= RemoveItem;
        }

        public void AddItem(GameItemInfo gameItemInfo)
        {
            _inventoryModel.AddItem(gameItemInfo);
        }

        private void AddNewItemView(GameItemInfo gameItemInfo)
        {
            _inventoryView.AddItem(gameItemInfo);
        }

        private void IncreaseItemQuantityView(int itemId, int quantity)
        {
            _inventoryView.IncreaseItemQuantity(itemId, quantity);
        }

        private void RemoveItem(int itemId)
        {
            _inventoryModel.RemoveItem(itemId);
        }
    }
}