using System;
using System.Collections.Generic;

namespace Inventory
{
    public class InventoryModel
    {
        public event Action<GameItemInfo> OnItemAdded;
        public event Action<int, int> OnItemQuantityChanged;

        private readonly List<GameItemInfo> _gameItemsInfo = new();

        public void AddItem(GameItemInfo newGameItemInfo)
        {
            foreach (GameItemInfo gameItemInfo in _gameItemsInfo)
            {
                if (gameItemInfo.Id == newGameItemInfo.Id)
                {
                    gameItemInfo.Quantity += newGameItemInfo.Quantity;
                    OnItemQuantityChanged?.Invoke(newGameItemInfo.Id, gameItemInfo.Quantity);
                    return;
                }
            }

            _gameItemsInfo.Add(new GameItemInfo(newGameItemInfo.Id, newGameItemInfo.ItemName, newGameItemInfo.Quantity, newGameItemInfo.Icon));
            
            OnItemAdded?.Invoke(newGameItemInfo);
        }

        public void RemoveItem(int itemId)
        {
            for (int i = 0; i < _gameItemsInfo.Count; i++)
            {
                if (_gameItemsInfo[i].Id == itemId)
                {
                    _gameItemsInfo.RemoveAt(i);
                }
            }
        }

        public bool TryGetAmmo(int ammoId)
        {
            foreach (GameItemInfo gameItemInfo in _gameItemsInfo)
            {
                if (gameItemInfo.Id != ammoId)
                {
                    continue;
                }
                
                gameItemInfo.Quantity--;
                OnItemQuantityChanged?.Invoke(gameItemInfo.Id, gameItemInfo.Quantity);
                return true;
            }

            return false;
        }
    }
}