namespace GameNext.GameNext.Code.SM.Gameplay.PC
{
    public class InAir : StateBase<PlayerController>, IPC_States
    {
        public void HandleTransition()
        {
            if (_core.conditions.groundHit)
                RequestTransition<Grounded>();
        }
        
        public void HandleX()
        {
            
        }

        public void HandleY()
        {
            _core.frameForce.y += -_core.stats.inAir.gravityAcceleration;
        }
    }
}