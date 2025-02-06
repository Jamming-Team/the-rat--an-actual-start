using UnityEngine;

namespace MeatAndSoap
{
    public class ApexModifiers : InAirModuleCommand, IVisitableMC<MCStatsData.ApexModifiers>
    {
        [SerializeField] private MCStatsData.ApexModifiers _data;
        
        public override void Execute()
        {
            var apexPoint = Mathf.InverseLerp(_data.threshold, 0f, Mathf.Abs(_mc.frameData.pastVelocity.y));
            
            var apexBonusX = _data.xAccelerationBonus * apexPoint;
            _stats.xMovement.acceleration += apexBonusX;
            
            var apexMinGravity = _stats.gravity.value * _data.minGravityModifier;
            _stats.gravity.value = Mathf.Lerp(apexMinGravity, _stats.gravity.value, apexPoint);
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
}