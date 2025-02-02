using UnityEngine;

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
            if (_core.frameInput.move.x == 0)
            {
                if (Mathf.Abs(_core.pastVelocity.x) > _core.stats.grounded.XtotalStopThreshold)
                {
                    _core.frameForce.x += -Mathf.Sign(_core.pastVelocity.x) * _core.stats.grounded.groundDeceleration;
                }
                else
                {
                    _core.NullifyVelocity(x: true);
                }
            }
            else
            {
                if (!_core.conditions.antiInputX)
                {
                    if (Mathf.Abs(_core.pastVelocity.x) < _core.stats.grounded.maxGroundSpeed)
                    {
                        _core.frameForce.x += _core.frameInput.move.x * _core.stats.grounded.groundAcceleration;
                    }
                }
                else
                {
                    _core.frameForce.x += -Mathf.Sign(_core.pastVelocity.x) * _core.stats.grounded.groundDeceleration;
                    _core.frameForce.x += _core.frameInput.move.x * _core.stats.grounded.groundAcceleration;
                }

            }
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