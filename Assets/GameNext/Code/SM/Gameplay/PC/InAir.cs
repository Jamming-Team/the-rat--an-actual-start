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
            
            if (_core.conditions.shouldJump)
                ExecuteJump();
        }
        
        private void ExecuteJump()
        {
            if (_core.pastVelocity.y < 0)
                _core.frameBurst.y += -_core.pastVelocity.y;
            
            _core.frameBurst.y += _core.stats.grounded.jumpForce;
            _core.conditions.hasJumpToConsume = false;
            _core.conditions.coyoteUsable = false;
            _core.markers.timeJumpWasPressed = float.MinValue;
        }
    }
}