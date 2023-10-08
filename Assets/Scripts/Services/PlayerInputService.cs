using Joystick_Pack.Scripts.Joysticks;
using UI;
using UnityEngine;

namespace Services
{
    public class PlayerInputService : MonoBehaviour
    {
        private FloatingJoystick _floatingJoystick;
        private ButtonsManager _buttonsManager;

        public Vector2 MoveDirection
        {
            get { return _floatingJoystick.Direction; }
        }

        public bool IsFireButtonDown
        {
            get { return _buttonsManager.IsFireButtonDown; }
        }

        private void Start()
        {
            _buttonsManager = FindObjectOfType<ButtonsManager>();
            _floatingJoystick = FindObjectOfType<FloatingJoystick>();
        }
    }
}
