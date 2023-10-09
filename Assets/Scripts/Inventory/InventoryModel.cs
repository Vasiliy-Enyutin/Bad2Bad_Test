using System;
using System.Collections.Generic;
using UnityEngine;

namespace Inventory
{
    public class InventoryModel
    {
        public event Action<GameItem> OnItemAdded;
        public event Action<int, int> OnItemQuantityIncreased;

        public List<GameItem> GameItems { get; } = new();

        public void AddItem(GameItem newGameItem)
        {
            foreach (GameItem gameItem in GameItems)
            {
                if (gameItem.Id == newGameItem.Id)
                {
                    gameItem.Quantity += newGameItem.Quantity;
                    Debug.Log(gameItem.Quantity);
                    Debug.Log("New game item quantity: " + newGameItem.Quantity);
                    OnItemQuantityIncreased?.Invoke(newGameItem.Id, gameItem.Quantity);
                    return;
                }
            }
            
            GameItems.Add(newGameItem);
            OnItemAdded?.Invoke(newGameItem);
        }
    }
}