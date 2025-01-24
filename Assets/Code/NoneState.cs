using UnityEngine;
using UnityEngine.InputSystem;

namespace Rat
{

    public class NoneState : StateBase<GC.States.InputMaps, GameInputManager>
    {

        public override void Init(GameInputManager context)
        {
            base.Init(context);
            stateName = GC.States.InputMaps.None;
        }

    }

}