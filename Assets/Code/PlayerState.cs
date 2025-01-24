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

    }

}