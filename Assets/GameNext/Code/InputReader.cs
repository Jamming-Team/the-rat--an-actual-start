using UnityEngine;
using UnityEngine.InputSystem;
using static MAS_InputActions;

namespace MeatAndSoap
{
    public interface IInputReader
    {
        void EnablePlayerActions();
        void DisablePlayerActions();
    }

    [CreateAssetMenu(fileName = "InputReader", menuName = "MeatNSoap_RFA/InputReader", order = 0)]
    public class InputReader : ScriptableObject, IInputReader, IPlayerActions
    {
        
        private MAS_InputActions _inputActions;
        
        public Vector2 move => _inputActions.Player.Move.ReadValue<Vector2>();
        public bool jumpPerformed => _inputActions.Player.Jump.WasPerformedThisFrame();
        public bool jumpIsBeingPressed => _inputActions.Player.Jump.inProgress;
        
        public void EnablePlayerActions()
        {
            if (_inputActions == null)
            {
                _inputActions = new MAS_InputActions();
                _inputActions.Player.SetCallbacks(this);
            }
            _inputActions.Enable();
        }

        public void DisablePlayerActions()
        {
            _inputActions.Disable();
        }

        public void OnMove(InputAction.CallbackContext context)
        {
        }

        public void OnJump(InputAction.CallbackContext context)
        {
        }
    }
}