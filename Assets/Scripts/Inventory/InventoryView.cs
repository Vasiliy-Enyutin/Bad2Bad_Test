using System;
using System.Collections.Generic;
using UnityEngine;

namespace Inventory
{
    public class InventoryView : MonoBehaviour
    {
        [SerializeField]
        private List<ItemButton> _itemButtons = null!;

        public event Action<int> OnRemoveButtonClicked; 

        private void OnEnable()
        {
            foreach (ItemButton itemButton in _itemButtons)
            {
                itemButton.OnRemoveClicked += RemoveItemClicked;
            }
        }

        private void OnDisable()
        {
            foreach (ItemButton itemButton in _itemButtons)
            {
                itemButton.OnRemoveClicked -= RemoveItemClicked;
            }        
        }

        public void AddItem(GameItemInfo newGameItemInfo)
        {
            foreach (ItemButton itemButton in _itemButtons)
            {
                if (!itemButton.ItemImage.gameObject.activeSelf)
                {
                    itemButton.Id = newGameItemInfo.Id;
                    itemButton.ItemImage.sprite = newGameItemInfo.Icon;
                    itemButton.QuantityText.text = newGameItemInfo.Quantity.ToString();

                    itemButton.ItemImage.gameObject.SetActive(true);
                    if (newGameItemInfo.Quantity > 1)
                    {
                        itemButton.QuantityText.enabled = true;
                    }
                    
                    break;
                }
            }
        }

        public void IncreaseItemQuantity(int itemId, int quantity)
        {
            foreach (ItemButton itemButton in _itemButtons)
            {
                if (itemButton.Id == itemId)
                {
                    itemButton.QuantityText.text = quantity.ToString();
                    itemButton.QuantityText.gameObject.SetActive(true);
                }
            }
        }

        private void RemoveItemClicked(ItemButton callingItemButton, int itemId)
        {
            ClearButton(callingItemButton);
            OnRemoveButtonClicked?.Invoke(itemId);
        }

        private void ClearButton(ItemButton callingItemButton)
        {
            callingItemButton.RemoveButton.gameObject.SetActive(false);
            callingItemButton.ItemImage.gameObject.SetActive(false);
            callingItemButton.QuantityText.gameObject.SetActive(false);
        }
    }
}