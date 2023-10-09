using UnityEngine;

namespace UI
{
    public class InventoryButton : MonoBehaviour
    {
        [SerializeField]
        private GameObject _background;
        
        public void UpdateInventoryShowState()
        {
            _background.SetActive(!_background.activeSelf);
        }
    }
}
