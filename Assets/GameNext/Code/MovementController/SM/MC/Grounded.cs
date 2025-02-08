using System.Collections.Generic;
using TriInspector;
using UnityEngine;
using static GC.MC;

namespace MeatAndSoap.SM.MC
{
    public class Grounded : StateBase<MovementController>, IPC_States, IVisitableMC<MCStatsData.Grounded>
    {
        [SerializeField] 
        private MCStatsData.Grounded _stats;
        private readonly MCStatsData.Grounded _frameStats = new();

        private readonly List<ICommandMCModule> _commandsList = new();
        
        public object statsRef => _frameStats;
        public MovementController coreRef => _core;

        public override void Init(MonoBehaviour core)
        {
            base.Init(core);
            GetComponentsInChildren(_commandsList);
            _commandsList.ForEach(x =>
            {
                x.Init(this);
            });
            // _stats.xMovement.acceleration = _stats.xMovement.maxSpeed;
            // _stats.deceleration = _stats.maxSpeed / 2f;
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
                else
                {
                    _core.NullifyVelocity(x: true);
                }
            }
            else
            {
                if (!_core.conditions[Conditions.AntiInputX])
                {
                    if (Mathf.Abs(_core.frameData.pastVelocity.x) < _frameStats.maxSpeed)
                    {
                        // Debug.Log(_frameStats.xMovement.acceleration);
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
            
            _core.frameData.frameForce.y += -_frameStats.gravityValue;
            

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
            _core.markers[Markers.RemainingJumpPotential] = _core.jumpData.jumpForce;
        }

        public void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }

        public void FillData(MCStatsData.Grounded data)
        {
            _stats = data;
        }
    }
}