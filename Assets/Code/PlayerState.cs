using UnityEngine;
using UnityEngine.InputSystem;

namespace Rat
{

    public class PlayerState : StateBase<GC.States.InputMaps, GameInputManager>
    {

        public override void Init(GameInputManager context)
        {
            base.Init(context);
            stateName = GC.States.InputMaps.Player;
        }

        private void OnInteract()
        {
            _context.player.interact?.Invoke();
        }
        
        private void OnJump()
        {
            _context.player.jump?.Invoke();
        }
        
        private void OnLook(InputValue value)
        {
            _context.player.look?.Invoke(value.Get<Vector2>());
        }
        
        private void OnMove(InputValue value)
        {
            _context.player.move?.Invoke(value.Get<float>());
        }
        
        private void OnPause()
        {
            _context.player.pause?.Invoke();
        }

        private void OnOverlook(InputValue value)
        {
            _context.player.overlook?.Invoke(value.isPressed);
        }
        
#if UNITY_EDITOR

        private void OnRestart()
        {
            _context.player.restart?.Invoke();
        }
        
#endif
        
        

    }

}