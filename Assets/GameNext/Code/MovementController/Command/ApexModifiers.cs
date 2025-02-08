using UnityEngine;

namespace MeatAndSoap
{
    public class ApexModifiers : MCModuleCommand<MCStatsData.IXMovement_x_Gravity>, IVisitableMC<MCStatsData.ApexModifiers>
    {
        [SerializeField] private MCStatsData.ApexModifiers _data;
        
        public override void Execute()
        {
            var apexPoint = Mathf.InverseLerp(_data.threshold, 0f, Mathf.Abs(_mc.frameData.pastVelocity.y));
            
            var apexBonusX = _data.xAccelerationBonus * apexPoint;
            _stats.acceleration += apexBonusX;
            
            var apexMinGravity = _stats.gravityValue * _data.minGravityModifier;
            _stats.gravityValue = Mathf.Lerp(apexMinGravity, _stats.gravityValue, apexPoint);
        }
        
        public void FillData(MCStatsData.ApexModifiers data)
        {
            _data = data;
        }

        public void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
    
    // public class EnvironmentDeceleration
}