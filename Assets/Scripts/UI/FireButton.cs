using UnityEngine;
using UnityEngine.EventSystems;

namespace UI
{
    public class FireButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        public bool IsButtonDown { get; private set; }
        
        public void OnPointerDown(PointerEventData eventData)
        {
            IsButtonDown = true;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            IsButtonDown = false;
        }
    }
}
