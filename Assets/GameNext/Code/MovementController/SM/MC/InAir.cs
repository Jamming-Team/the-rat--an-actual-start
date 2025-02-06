using System.Collections.Generic;
using UnityEngine;
using static GC.MC;

namespace MeatAndSoap.SM.MC
{
    public class InAir : StateBase<MovementController>, IPC_States, IVisitableMC<MCStatsData.InAir>
    {
        [SerializeField]
        private MCStatsData.InAir _stats;
        private readonly MCStatsData.InAir _frameStats = new();
        
        private readonly List<InAirModuleCommand> _commandsList = new();

        
        public override void Init(MonoBehaviour core)
        {
            base.Init(core);
            GetComponentsInChildren(_commandsList);
            _commandsList.ForEach(x =>
            {
                x.Init(_frameStats, _core);
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
            // _frameStats.xMovement.deceleration *= Mathf.InverseLerp(Mathf.Abs(_stats.xMovement.maxSpeed), 0, Mathf.Abs(_core.frameData.pastVelocity.x));
            if (_core.frameInput.move.x == 0)
            {
                if (Mathf.Abs(_core.frameData.pastVelocity.x) >= _frameStats.xMovement.stopThreshold)
                    _core.frameData.frameForce.x += -Mathf.Sign(_core.frameData.pastVelocity.x) * _frameStats.xMovement.deceleration;
            }
            else
            {
                if (!_core.conditions[Conditions.AntiInputX])
                {
                    if (Mathf.Abs(_core.frameData.pastVelocity.x) < _frameStats.xMovement.maxSpeed)
                    {
                        _core.frameData.frameForce.x += _core.frameInput.move.x * _frameStats.xMovement.acceleration;
                    }
                }
                else
                {
                    _core.frameData.frameForce.x += -Mathf.Sign(_core.frameData.pastVelocity.x) * _frameStats.xMovement.deceleration;
                    _core.frameData.frameForce.x += _core.frameInput.move.x * _frameStats.xMovement.acceleration;
                }

            }
            
            // Y-Axis
            
            if (_core.frameData.pastVelocity.y >= -_frameStats.gravity.maxFallSpeed)
                _core.frameData.frameForce.y += -_frameStats.gravity.value;
            
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
            _core.markers[Markers.RemainingJumpPotential] = _core.jumpData.jumpForce;
        }

        public void FillData(MCStatsData.InAir data)
        {
            _stats = data;
        }

        public void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}