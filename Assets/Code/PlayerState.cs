using UnityEngine;

public class PlayerState : StateBase<GC.States.InputMaps, GameInputManager>
{
    
    public override void Init(GameInputManager context)
    {
        base.Init(context);
        stateName = GC.States.InputMaps.Player;
    }
    
    protected override void OnEnter()
    {
        throw new System.NotImplementedException();
    }

    protected override void OnExit()
    {
        throw new System.NotImplementedException();
    }
}
