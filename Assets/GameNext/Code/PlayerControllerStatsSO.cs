using UnityEngine;
using UnityEngine.Serialization;

namespace GameNext
{
    [CreateAssetMenu(fileName = "PCStats", menuName = "MeatNSoap/PCStats", order = 1)]
    public class PlayerControllerStatsSO : ScriptableObject
    {
        public StatsData data = new StatsData();
    }
    
    [System.Serializable]
    public class StatsData
    {
        public General general;
        public Grounded grounded;
        public InAir inAir;
            
        [System.Serializable]
        public class General
        {
            public LayerMask groundLayer;
            public float collisionDetectionDistance = 0.1f;
        }

        [System.Serializable]
        public class Grounded
        {
            [FormerlySerializedAs("totalStopThreshold")] public float XtotalStopThreshold = 0.02f;
                
            public float groundAcceleration = 3f;
            public float maxGroundSpeed = 6f;
            public float groundDeceleration = 6f;
                
            public float jumpForce = 5f;
            public float jumpBufferTime = 0.3f;
        }

        [System.Serializable]
        public class InAir
        {
            public float XtotalStopThreshold = 0.02f;
                
            public float airXAcceleration = 3f;
            public float airXDeceleration = 3f;
            public float airXMaxSpeed = 6f;
                
            public float gravityAcceleration = 8f;
            public float fallMaxSpeed = 12f;
                
            public float coyoteTime = 0.3f;
        }
            
            
    }
}
