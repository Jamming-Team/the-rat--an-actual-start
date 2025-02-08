using UnityEngine;
using UnityEngine.Serialization;

namespace MeatAndSoap
{
    [CreateAssetMenu(fileName = "MCStatsSO", menuName = "MeatNSoap_RFA/MCStatsSO", order = 0)]
    public class MCStatsSO : ScriptableObject
    {
        // [EditorAttributes.Title("States", drawLine: true)]
        public MCStatsData.Grounded grounded = new();
        public MCStatsData.InAir inAir = new();
        
        // [EditorAttributes.Title("Modules", drawLine: true)]
        public MCStatsData.NonParabolicJump nonParabolicJump = new();
        public MCStatsData.VariableJumpHeight variableJumpHeight = new();
        public MCStatsData.ApexModifiers apexModifiers = new();
        
        // public MCStatsData.NonLinearXAcceleration nonLinearXAccelerationGrounded = new();
        // public MCStatsData.NonLinearXAcceleration nonLinearXAccelerationInAir = new();

        
        // [EditorAttributes.Title("Other", drawLine: true)]
        public MCStatsData.JumpData jump = new();
        
        void OnValidate()
        {
        //     grounded.ixMovement.timeToMaxSpeed = "Фул разгон за: " + (grounded.ixMovement.maxSpeed / grounded.ixMovement.acceleration).ToString();
        //     inAir.ixMovement.timeToMaxSpeed = "Фул разгон за: " + (inAir.ixMovement.maxSpeed / inAir.ixMovement.acceleration).ToString();
        }
    }
}