using System;
using System.Collections.Generic;
using UnityEngine;

namespace Inventory
{
    public class InventoryModel
    {
        public event Action<GameItemInfo> OnItemAdded;
        public event Action<int, int> OnItemQuantityIncreased;

        public List<GameItemInfo> GameItemsInfo { get; } = new();

        public void AddItem(GameItemInfo newGameItemInfo)
        {
            foreach (GameItemInfo gameItemInfo in GameItemsInfo)
            {
                if (gameItemInfo.Id == newGameItemInfo.Id)
                {
                    gameItemInfo.Quantity += newGameItemInfo.Quantity;
                    OnItemQuantityIncreased?.Invoke(newGameItemInfo.Id, gameItemInfo.Quantity);
                    return;
                }
            }

            GameItemsInfo.Add(new GameItemInfo(newGameItemInfo.Id, newGameItemInfo.ItemName, newGameItemInfo.Quantity, newGameItemInfo.Icon));
            
            OnItemAdded?.Invoke(newGameItemInfo);
        }

        public void RemoveItem(int itemId)
        {
            foreach (GameItemInfo gameItemInfo in GameItemsInfo)
            {
                Debug.Log(gameItemInfo.ItemName);
            }
            
            for (int i = GameItemsInfo.Count - 1; i >= 0; i--)
            {
                if (GameItemsInfo[i].Id == itemId)
                {
                    GameItemsInfo.RemoveAt(i);
                }
            }
            
            foreach (GameItemInfo gameItemInfo in GameItemsInfo)
            {
                Debug.Log(gameItemInfo.ItemName);
            }
        }
    }
}