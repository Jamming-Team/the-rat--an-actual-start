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
            grounded.xMovement.timeToMaxSpeed = "Фул разгон за: " + (grounded.xMovement.maxSpeed / grounded.xMovement.acceleration).ToString();
            inAir.xMovement.timeToMaxSpeed = "Фул разгон за: " + (inAir.xMovement.maxSpeed / inAir.xMovement.acceleration).ToString();
        }
    }
}