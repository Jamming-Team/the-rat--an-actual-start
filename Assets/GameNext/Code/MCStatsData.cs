namespace GameNext
{
    public class MCStatsData
    {
        // Parts

        public interface IXMovement 
        {
            public float acceleration { get; set; }
            public float deceleration { get; set; }
            public float maxSpeed { get; set; }
            public float stopThreshold { get; set; }
        }
        
        public interface IGravity
        {
            public float gravity { get; set; }
        }
        
        public interface ICopyable<in T_CopyType>
        {
            public void Copy(T_CopyType copyObject);
        }
        
        public interface IXMovementXGravity : IXMovement, IGravity { }

        // States
        
        [System.Serializable]
        public class Grounded
        {
            public float acceleration = 3f;
            public float deceleration = 6f;
            public float maxSpeed = 6f;
            public float stopThreshold = 0.1f;

            public float gravity = 9.81f;


            public void Copy(Grounded copyObject)
            {
                acceleration = copyObject.acceleration;
                deceleration = copyObject.deceleration;
                maxSpeed = copyObject.maxSpeed;
                gravity = copyObject.gravity;
            }
        }
        
        [System.Serializable]
        public class InAir
        {
            public float acceleration = 2f;
            public float deceleration = 4f;
            public float maxSpeed = 6f;
            public float stopThreshold = 0.1f;

            public float gravity = 9.81f;
            public float maxFallSpeed = 6f;
            
            
            public void Copy(InAir copyObject)
            {
                acceleration = copyObject.acceleration;
                deceleration = copyObject.deceleration;
                maxSpeed = copyObject.maxSpeed;
                gravity = copyObject.gravity;
                maxFallSpeed = copyObject.maxFallSpeed;
            }
        }
        
        // Modules

        [System.Serializable]
        public class CoyoteTime
        {
            public float time = 0.3f;
        }
        
        [System.Serializable]
        public class JumpBuffer
        {
            public float time = 0.3f;
        }

        [System.Serializable]
        public class NonParabolicJump
        {
            public float lowGravityModifier = 0.6f;
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