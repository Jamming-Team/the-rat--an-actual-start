using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using static GC.MC;

namespace GameNext.GameNext.Code.SM.Gameplay.PC
{
    public class InAir : StateBase<MovementController>, IPC_States
    {
        [SerializeField]
        private MCStatsData.InAir _stats = new();
        private readonly MCStatsData.InAir _frameStats = new();
        
        private readonly List<InAirModuleCommand> _commandsList = new();

        
        public override void Init(MonoBehaviour core)
        {
            base.Init(core);
            GetComponentsInChildren(_commandsList);
            _commandsList.ForEach(x =>
            {
                x.Init(_frameStats, _core.frameData, _core.frameInput);
            });
        }

        public void HandleTransition()
        {
            if (_core.frameCollisions.groundHit)
                RequestTransition<Grounded>();
        }
        
        public void HandleModules()
        {
            _frameStats.Copy(_stats);
            _commandsList.ForEach(x => x.Execute());
        }
        
        public void HandleInnerForce()
        {
            _frameStats.deceleration *= Mathf.InverseLerp(Mathf.Abs(_stats.maxSpeed), 0, Mathf.Abs(_core.frameData.pastVelocity.x));
            if (_core.frameInput.move.x == 0)
            {
                if (Mathf.Abs(_core.frameData.pastVelocity.x) >= _frameStats.stopThreshold)
                    _core.frameData.frameForce.x += -Mathf.Sign(_core.frameData.pastVelocity.x) * _frameStats.deceleration;
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
            
            if (_core.frameData.pastVelocity.y >= -_frameStats.maxFallSpeed)
                _core.frameData.frameForce.y += -_frameStats.gravity;
            
            if (_core.conditions[Conditions.ShouldJumpInAir])
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