using System;
using Descriptors;
using PlayerLogic;
using UnityEngine;

namespace Inventory
{
    [RequireComponent(typeof(Player))]
    public class InventoryController : MonoBehaviour
    {
        private const int AMMO_ID = 0;

        private Player _player;
        private readonly InventoryModel _inventoryModel = new();

        private InventoryView _inventoryView;

        private void Awake()
        {
            _inventoryView = FindObjectOfType<InventoryView>();
            _player = GetComponent<Player>();
        }

        private void OnEnable()
        {
            _inventoryModel.OnItemAdded += AddNewItemView;
            _inventoryModel.OnItemQuantityChanged += ChangeItemQuantity;

            _inventoryView.OnRemoveItem += RemoveItem;
        }

        private void OnDisable()
        {
            _inventoryModel.OnItemAdded -= AddNewItemView;
            _inventoryModel.OnItemQuantityChanged -= ChangeItemQuantity;
            
            _inventoryView.OnRemoveItem -= RemoveItem;
        }

        private void Start()
        {
            AddBaseAmmo();
        }

        public void AddItem(GameItemInfo gameItemInfo)
        {
            _inventoryModel.AddItem(gameItemInfo);
        }

        public bool TryGetAmmo()
        {
            return _inventoryModel.TryGetAmmo(_player.AmmoItemDescriptor.Id);
        }

        private void AddNewItemView(GameItemInfo gameItemInfo)
        {
            _inventoryView.AddItem(gameItemInfo);
        }

        private void ChangeItemQuantity(int itemId, int quantity)
        {
            _inventoryView.ChangeItemQuantity(itemId, quantity);
        }

        private void RemoveItem(int itemId)
        {
            _inventoryModel.RemoveItem(itemId);
        }

        private void AddBaseAmmo()
        {
            GameItemInfo gameItemInfo = new(_player.AmmoItemDescriptor.Id,
                _player.AmmoItemDescriptor.ItemName, 
                _player.PlayerDescriptor.BaseAmmoQuantity,
                _player.AmmoItemDescriptor.Icon);
            
            _inventoryModel.AddItem(gameItemInfo);
        }
    }
}