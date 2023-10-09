using UnityEngine;

namespace Inventory
{
    public class GameItemInfo
    {
        public GameItemInfo(int id, string itemName, int quantity, Sprite icon)
        {
            Id = id;
            ItemName = itemName;
            Quantity = quantity;
            Icon = icon;
        }
        
        public int Id { get; set; }
        public string ItemName { get; set; }
        public int Quantity { get; set; }
        public Sprite Icon { get; set; }
    }
}