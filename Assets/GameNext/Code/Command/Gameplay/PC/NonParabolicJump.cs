using UnityEngine;

namespace GameNext
{
    public class NonParabolicJump : InAirModuleCommand, IVisitableMC<MCStatsData.NonParabolicJump>
    {
        [SerializeField] private MCStatsData.NonParabolicJump _data;
        
        public override void Execute()
        {
            if (_mc.frameData.pastVelocity.y > 0f)
            {
                _stats.gravity.value *= _data.lowGravityModifier;
            }
            else
            {
                _stats.gravity.value *= _data.highGravityModifier;
            }
        }
        
        public void FillData(MCStatsData.NonParabolicJump data)
        {
            _data = data;
        }

        public void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}