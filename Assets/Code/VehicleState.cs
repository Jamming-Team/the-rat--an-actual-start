using UnityEngine;
using UnityEngine.InputSystem;

public class VehicleState : StateBase<GC.States.InputMaps, GameInputManager>
{
    
    public override void Init(GameInputManager context)
    {
        base.Init(context);
        stateName = GC.States.InputMaps.Vehicle;
    }

    private void OnInteract()
    {
        _context.vehicle.interact?.Invoke();
    }
    
}