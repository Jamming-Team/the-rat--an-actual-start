using System.Collections.Generic;
using UnityEngine;
using static GC.MC;

namespace GameNext.GameNext.Code.SM.Gameplay.PC
{
    public class Grounded : StateBase<MovementController>, IPC_States
    {
        [SerializeField] 
        private MCStatsData.Grounded _stats = new();
        private readonly MCStatsData.Grounded _frameStats = new();

        private readonly List<GroundedModuleCommands> _commandsList = new();
        
        
        public override void Init(MonoBehaviour core)
        {
            base.Init(core);
            GetComponentsInChildren(_commandsList);
            _commandsList.ForEach(x =>
            {
                x.Init(_frameStats, _core.frameData, _core.frameInput);
            });
        }
        
        protected override void OnEnter()
        {
            base.OnEnter();
            _core.conditions[Conditions.CoyoteUsable] = true;
            // _core.conditions.bufferedJumpUsable = true;
        }

        protected override void OnExit()
        {
            base.OnExit();
            _core.markers[Markers.TimeLeftGround] = _core.frameData.time;
            // _core.conditions.bufferedJumpUsable = false;
        }

        public void HandleTransition()
        {
            if (!_core.frameCollisions.groundHit)
                RequestTransition<InAir>();
        }

        public void HandleModules()
        {
            _frameStats.Copy(_stats);
            _commandsList.ForEach(x => x.Execute());
        }
        
        public void HandleInnerForce()
        {
            // X-Axis
            
            if (_core.frameInput.move.x == 0)
            {
                if (Mathf.Abs(_core.frameData.pastVelocity.x) >= _frameStats.stopThreshold)
                {
                    _core.frameData.frameForce.x += -Mathf.Sign(_core.frameData.pastVelocity.x) * _frameStats.deceleration;
                }
            }
            else
            {
                if (!_core.conditions[Conditions.AntiInputX])
                {
                    if (Mathf.Abs(_core.frameData.pastVelocity.x) < _frameStats.maxSpeed)
                    {
                        _core.frameData.frameForce.x += _core.frameInput.move.x * _frameStats.acceleration;
                    }
                }
                else
                {
                    _core.frameData.frameForce.x += -Mathf.Sign(_core.frameData.pastVelocity.x) * _frameStats.deceleration;
                    _core.frameData.frameForce.x += _core.frameInput.move.x * _frameStats.acceleration;
                }

            }
            
            // Y-Axis
            
            _core.frameData.frameForce.y += -_frameStats.gravity;

            if (_core.conditions[Conditions.ShouldJumpGrounded])
                ExecuteJump();
        }

        private void ExecuteJump()
        {
            if (_core.frameData.pastVelocity.y < 0)
                _core.frameData.frameBurst.y += -_core.frameData.pastVelocity.y;
            
            _core.frameData.frameBurst.y += _core.jumpData.jumpForce;
            _core.conditions[Conditions.HasJumpToConsume] = false;
            _core.conditions[Conditions.CoyoteUsable] = false;
            _core.markers[Markers.TimeJumpWasPressed] = float.MinValue;
        }
        
    }
}