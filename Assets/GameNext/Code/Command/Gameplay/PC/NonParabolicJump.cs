using UnityEngine;

namespace GameNext
{
    public class NonParabolicJump : MCModuleCommand<MCStatsData.Gravity>
    {
        [SerializeField] private MCStatsData.NonParabolicJump _data;
        
        public override void Execute()
        {
            if (_frameData.pastVelocity.y > 0f)
            {
                _stats.value *= _data.lowGravityModifier;
            }
        }
    }
}