using UnityEngine;

namespace GameNext.GameNext.Code.SM.Gameplay.PC
{
    public class Grounded : StateBase<PlayerController>, IPC_States
    {
        public void HandleTransition()
        {
            if (!_core.conditions.groundHit)
                RequestTransition<InAir>();
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
            if (_core.conditions.jumpToConsume)
                ExecuteJump();
            // throw new System.NotImplementedException();
        }

        private void ExecuteJump()
        {
            _core.frameBurst.y += _core.stats.grounded.jumpForce;
            _core.conditions.jumpToConsume = false;
        }
        
    }
}