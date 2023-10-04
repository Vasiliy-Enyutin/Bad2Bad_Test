using Joystick_Pack.Scripts.Joysticks;
using UnityEngine;

namespace PlayerLogic
{
    public class PlayerInputService : MonoBehaviour
    {
        private FloatingJoystick _floatingJoystick;
        
        private void Start()
        {
            _floatingJoystick = FindObjectOfType<FloatingJoystick>();
        }

        public Vector2 MoveDirection
        {
            get { return _floatingJoystick.Direction; }
        }
    }
}
