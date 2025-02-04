using UnityEngine;

namespace GameNext
{
    public class VariableJumpHeight : InAirModuleCommand, IVisitableMC<MCStatsData.VariableJumpHeight>
    {
        [SerializeField] private MCStatsData.VariableJumpHeight _data;
        
        public override void Execute()
        {
            if (_mc.markers[GC.MC.Markers.RemainingJumpPotential] > 0f)
            {
                if (_mc.frameData.pastVelocity.y <= 0f)
                {
                    _mc.markers[GC.MC.Markers.RemainingJumpPotential] = 0f;
                }
                else
                {
                    if (!_mc.frameInput.jumpHeld)
                    {
                        _mc.frameData.frameBurst.y += -_mc.markers[GC.MC.Markers.RemainingJumpPotential] * _data.antiJumpBurstModifier;
                        _mc.markers[GC.MC.Markers.RemainingJumpPotential] = 0f;
                    }
                    else
                    {
                        _mc.markers[GC.MC.Markers.RemainingJumpPotential] += -_stats.gravity.value * Time.fixedDeltaTime;
                    }
                }
            }
        }
        
        public void FillData(MCStatsData.VariableJumpHeight data)
        {
            _data = data;
        }

        public void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}