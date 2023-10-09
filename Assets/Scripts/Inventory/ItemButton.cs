using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Inventory
{
    public class ItemButton : MonoBehaviour
    {
        [SerializeField]
        private Image _itemImage;
        [SerializeField]
        private Button _removeButton;
        [SerializeField]
        private TextMeshProUGUI _quantityText;

        public int Id { get; set; } = -1;

        public Image ItemImage
        {
            get { return _itemImage; }
            set { _itemImage = value; }
        }

        public Button RemoveButton
        {
            get { return _removeButton; }
            set { _removeButton = value; }
        }

        public TextMeshProUGUI QuantityText
        {
            get { return _quantityText; }
            set { _quantityText = value; }
        }

        public void OnItemButtonClicked()
        {
            if (_itemImage.enabled)
            {
                _removeButton.enabled = true;
            }
        }
    }
}
