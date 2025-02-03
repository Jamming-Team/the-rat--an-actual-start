using UnityEngine;

namespace GameNext
{
    public class NonParabolicJump : InAirModuleCommand
    {
        [SerializeField] private MCStatsData.NonParabolicJump _data;
        
        public override void Execute()
        {
            if (_frameData.pastVelocity.y > 0f)
            {
                _stats.gravity *= _data.lowGravityModifier;
            }
        }
    }
}