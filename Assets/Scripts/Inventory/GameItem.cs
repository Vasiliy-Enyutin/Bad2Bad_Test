using UnityEngine;

namespace Inventory
{
    public class GameItem : MonoBehaviour
    {
        public int Id { get; set; }
        public string ItemName { get; set; }
        public int Quantity { get; set; }
        public Sprite Icon { get; set; }
    }
}