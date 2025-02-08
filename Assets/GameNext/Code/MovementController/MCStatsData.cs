using System.Globalization;
// using EditorAttributes;
using TriInspector;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

namespace MeatAndSoap
{
    public class MCStatsData
    {
        // Parts

        public interface IXMovement
        {
            // [HelpBox(nameof(timeToMaxSpeed), MessageMode.None, StringInputMode.Dynamic)]
            public float acceleration { get; set; }
            public float deceleration { get; set; }
            public float maxSpeed { get; set; }
            public float stopThreshold { get; set; }

            
            // public float acceleration { get; set; } = 6f;
            // public float deceleration = 12f;
            // public float maxSpeed = 6f;
            // public float stopThreshold = 0.1f;
            //
            // private float _test;


            
            // [HideInInspector]
            // public string timeToMaxSpeed = "default_text";
            
            // public void Copy(XMovement copyObject)
            // {
            //     acceleration = copyObject.acceleration;
            //     deceleration = copyObject.deceleration;
            //     maxSpeed = copyObject.maxSpeed;
            //     stopThreshold = copyObject.stopThreshold;
            // }
        }
        
        public interface IGravity
        {
            public float gravityValue { get; set; }
            public float maxFallSpeed { get; set; }
            
        }
        
        public interface IXMovement_x_Gravity : IXMovement, IGravity {}
        
        interface ICopyable<in T_CopyType>
        {
            public void Copy(T_CopyType copyObject);
        }
        

        // States
        
        [System.Serializable]
        public class Grounded : IXMovement_x_Gravity, ICopyable<Grounded>
        {
            [ShowInInspector]
            public float acceleration { get; set; } = 6f;
            [ShowInInspector]
            public float deceleration { get; set; } = 12f;
            [ShowInInspector]
            public float maxSpeed { get; set; } = 6f;
            [ShowInInspector]
            public float stopThreshold { get; set; } = .1f;
            
            [ShowInInspector]
            public float gravityValue { get; set; } = 9.81f;

            [ShowInInspector]
            public float maxFallSpeed { get; set; } = 12f;
            

            public void Copy(Grounded copyObject)
            {
                acceleration = copyObject.acceleration;
                deceleration = copyObject.deceleration;
                maxSpeed = copyObject.maxSpeed;
                stopThreshold = copyObject.stopThreshold;
                
                gravityValue = copyObject.gravityValue;
                maxFallSpeed = copyObject.maxFallSpeed;
            }
        }
        
        [System.Serializable]
        public class InAir : IXMovement_x_Gravity, ICopyable<InAir>
        {
            [ShowInInspector]
            public float acceleration { get; set; } = 6f;
            [ShowInInspector]
            public float deceleration { get; set; } = 12f;
            [ShowInInspector]
            public float maxSpeed { get; set; } = 6f;
            [ShowInInspector]
            public float stopThreshold { get; set; } = .1f;
            
            [ShowInInspector]
            public float gravityValue { get; set; } = 9.81f;
            [ShowInInspector]
            public float maxFallSpeed { get; set; } = 12f;
            

            public void Copy(InAir copyObject)
            {
                acceleration = copyObject.acceleration;
                deceleration = copyObject.deceleration;
                maxSpeed = copyObject.maxSpeed;
                stopThreshold = copyObject.stopThreshold;
                
                gravityValue = copyObject.gravityValue;
                maxFallSpeed = copyObject.maxFallSpeed;
            }
        }
        
        // Modules

        [System.Serializable]
        public class NonParabolicJump
        {
            public float lowGravityModifier = 0.6f;
            public float highGravityModifier = 1.6f;
        }
        
        [System.Serializable]
        public class NonLinearXAcceleration
        {
            public float timeTillFullVelocity = 0.6f;
        }
        
        [System.Serializable]
        public class VariableJumpHeight
        {
            public float antiJumpBurstModifier = 0.6f;
        }
        
        [System.Serializable]
        public class ApexModifiers
        {
            public float threshold = 2f;
            public float xAccelerationBonus = 2f;
            public float minGravityModifier = 0.3f;
        }
        
        // Other
        
        [System.Serializable]
        public class JumpData
        {
            public float jumpForce = 5f;
            public float jumpBufferTime = 0.3f;
            public float coyoteTime = 0.3f;
        }
        
    }
}