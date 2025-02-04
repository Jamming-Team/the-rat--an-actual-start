using UnityEngine;

namespace GameNext
{
    public class NonParabolicJump : MCModuleCommand<MCStatsData.Gravity>, IVisitableMC<MCStatsData.NonParabolicJump>
    {
        [SerializeField] private MCStatsData.NonParabolicJump _data;
        
        public override void Execute()
        {
            if (_frameData.pastVelocity.y > 0f)
            {
                _stats.value *= _data.lowGravityModifier;
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