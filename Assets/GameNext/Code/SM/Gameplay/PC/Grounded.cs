using UnityEngine;

namespace GameNext.GameNext.Code.SM.Gameplay.PC
{
    public class Grounded : StateBase<PlayerController>, IPC_States
    {

        protected override void OnEnter()
        {
            base.OnEnter();
            _core.conditions.coyoteUsable = true;
            // _core.conditions.bufferedJumpUsable = true;
        }

        protected override void OnExit()
        {
            base.OnExit();
            _core.markers.timeLeftGround = _core.time;
            // _core.conditions.bufferedJumpUsable = false;
        }

        public void HandleTransition()
        {
            if (!_core.conditions.groundHit)
                RequestTransition<InAir>();
        }
        
        public void HandleX()
        {
            if (_core.frameInput.move.x == 0)
            {
                if (Mathf.Abs(_core.pastVelocity.x) > _core.stats.inAir.XtotalStopThreshold)
                {
                    _core.frameForce.x += -Mathf.Sign(_core.pastVelocity.x) * _core.stats.inAir.airXDeceleration;
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
                    if (Mathf.Abs(_core.pastVelocity.x) < _core.stats.inAir.airXMaxSpeed)
                    {
                        _core.frameForce.x += _core.frameInput.move.x * _core.stats.inAir.airXAcceleration;
                    }
                }
                else
                {
                    _core.frameForce.x += -Mathf.Sign(_core.pastVelocity.x) * _core.stats.inAir.airXDeceleration;
                    _core.frameForce.x += _core.frameInput.move.x * _core.stats.inAir.airXAcceleration;
                }

            }
        }

        public void HandleY()
        {
            _core.frameForce.y += -_core.stats.inAir.gravityAcceleration;

            if (_core.conditions.shouldJump)
                ExecuteJump();
            // throw new System.NotImplementedException();
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