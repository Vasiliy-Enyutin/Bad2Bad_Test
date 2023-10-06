using UnityEngine;

namespace UI
{
    public class ButtonsManager : MonoBehaviour
    {
        [SerializeField]
        private FireButton _fireButton;

        public bool IsFireButtonDown
        {
            get { return _fireButton.IsButtonDown; }
        }
    }
}
