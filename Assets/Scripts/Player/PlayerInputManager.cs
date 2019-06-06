using Hel.Events.CustomEvents;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Hel.Inputs
{
    /// <summary>
    /// Used to handle the player's input.
    /// </summary>
    public class PlayerInputManager : MonoBehaviour
    {
        [Required] [SerializeField] private IntEvent onNumberKeyPressed = null;

        private static bool isInputBlocked = false;

        private static readonly KeyCode[] numberKeys = new KeyCode[]
        {
            KeyCode.Alpha1,
            KeyCode.Alpha2,
            KeyCode.Alpha3,
            KeyCode.Alpha4,
            KeyCode.Alpha5,
            KeyCode.Alpha6,
            KeyCode.Alpha7,
            KeyCode.Alpha8,
            KeyCode.Alpha9,
            KeyCode.Alpha0,
        };

        public static Vector2 MovementInput
        {
            get
            {
                if (isInputBlocked) { return new Vector2(); }

                return new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            }
        }
        public static bool MovementKeyDown
        {
            get
            {
                if (isInputBlocked) { return false; }

                return (
                    Input.GetKeyDown(KeyCode.W) ||
                    Input.GetKeyDown(KeyCode.A) ||
                    Input.GetKeyDown(KeyCode.S) ||
                    Input.GetKeyDown(KeyCode.D));
            }
        }
        public static bool LeftClickPressed
        {
            get
            {
                if (isInputBlocked) { return false; }

                return Input.GetMouseButtonDown(0);
            }
        }
        public static bool LeftClickHeld
        {
            get
            {
                if (isInputBlocked) { return false; }

                return Input.GetMouseButton(0);
            }
        }
        public static bool LeftClickReleased
        {
            get
            {
                if (isInputBlocked) { return false; }

                return Input.GetMouseButtonUp(0);
            }
        }
        public static bool RightClickPressed
        {
            get
            {
                if (isInputBlocked) { return false; }

                return Input.GetMouseButtonDown(1);
            }
        }
        public static bool RightClickHeld
        {
            get
            {
                if (isInputBlocked) { return false; }

                return Input.GetMouseButton(1);
            }
        }
        public static bool RightClickReleased
        {
            get
            {
                if (isInputBlocked) { return false; }

                return Input.GetMouseButtonUp(1);
            }
        }
        public static bool AnyKeyDown
        {
            get
            {
                if (isInputBlocked) { return false; }

                return Input.anyKeyDown;
            }
        }
        public static bool AnyKey
        {
            get
            {
                if (isInputBlocked) { return false; }

                return Input.anyKey;
            }
        }

        private void Update()
        {
            for (int i = 0; i < numberKeys.Length; i++)
            {
                if (Input.GetKeyDown(numberKeys[i]))
                {
                    onNumberKeyPressed.Raise(i);
                }
            }
        }

        public void BlockInput(bool block)
        {
            isInputBlocked = block;
        }
    }
}
