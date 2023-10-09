using System.Collections.Generic;
using UnityEngine;

namespace Inventory
{
    public class InventoryView : MonoBehaviour
    {
        [SerializeField]
        private List<ItemButton> _itemButtons = null!;

        public void AddItem(GameItem newGameItem)
        {
            foreach (ItemButton itemButton in _itemButtons)
            {
                if (!itemButton.ItemImage.gameObject.activeSelf)
                {
                    itemButton.Id = newGameItem.Id;
                    itemButton.ItemImage.sprite = newGameItem.Icon;
                    itemButton.QuantityText.text = newGameItem.Quantity.ToString();

                    itemButton.ItemImage.gameObject.SetActive(true);
                    if (newGameItem.Quantity > 1)
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
    }
}