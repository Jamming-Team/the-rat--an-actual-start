using UnityEngine;

namespace MeatAndSoap
{
    public class NonParabolicJump : MCModuleCommand<MCStatsData.IGravity>, IVisitableMC<MCStatsData.NonParabolicJump>
    {
        [SerializeField] private MCStatsData.NonParabolicJump _data;
        
        public override void Execute()
        {
            if (_mc.frameData.pastVelocity.y > 0f)
            {
                _stats.gravityValue *= _data.lowGravityModifier;
            }
            else
            {
                _stats.gravityValue *= _data.highGravityModifier;
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