using System.Globalization;
using EditorAttributes;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

namespace GameNext
{
    public class MCStatsData
    {
        // Parts

        [System.Serializable]
        public class XMovement : ICopyable<XMovement>
        {
            [HelpBox(nameof(timeToMaxSpeed), MessageMode.None, StringInputMode.Dynamic)]
            public float acceleration = 6f;
            public float deceleration = 12f;
            public float maxSpeed = 6f;
            public float stopThreshold = 0.1f;
            
            [HideInInspector]
            public string timeToMaxSpeed = "default_text";
            
            public void Copy(XMovement copyObject)
            {
                acceleration = copyObject.acceleration;
                deceleration = copyObject.deceleration;
                maxSpeed = copyObject.maxSpeed;
                stopThreshold = copyObject.stopThreshold;
            }
        }
        
        [System.Serializable]
        public class Gravity
        {
            public float value = 9.81f;
            public float maxFallSpeed = 6f;
            
            public void Copy(Gravity copyObject)
            {
                value = copyObject.value;
                maxFallSpeed = copyObject.maxFallSpeed;
            }
        }
        
        interface ICopyable<in T_CopyType>
        {
            public void Copy(T_CopyType copyObject);
        }
        

        // States
        
        [System.Serializable]
        public class Grounded : ICopyable<Grounded>
        {
            public XMovement xMovement = new XMovement
            {
                acceleration = 6f,
                deceleration = 12f,
                maxSpeed = 6f,
                stopThreshold = 0.1f,
            };

            public Gravity gravity = new Gravity
            {
                value = 9.81f,
                maxFallSpeed = 6f,
            };
            

            public void Copy(Grounded copyObject)
            {
                xMovement.Copy(copyObject.xMovement);
                gravity.Copy(copyObject.gravity);
            }
        }
        
        [System.Serializable]
        public class InAir : ICopyable<InAir>
        {
            public XMovement xMovement = new XMovement
            {
                acceleration = 3f,
                deceleration = 3f,
                maxSpeed = 6f,
                stopThreshold = 0.1f,
            };

            public Gravity gravity = new Gravity
            {
                value = 9.81f,
                maxFallSpeed = 9f,
            };
            
            
            public void Copy(InAir copyObject)
            {
                xMovement.Copy(copyObject.xMovement);
                gravity.Copy(copyObject.gravity);
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