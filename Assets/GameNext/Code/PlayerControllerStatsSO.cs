using UnityEngine;

namespace GameNext
{
    [CreateAssetMenu(fileName = "PCStats", menuName = "MeatNSoap/PCStats", order = 1)]
    public class PlayerControllerStatsSO : ScriptableObject
    {
        public StatsData data = new StatsData();

        [System.Serializable]
        public class StatsData
        {
            public float groundAcceleration = 3f;
            public float maxGroundSpeed = 6f;
            public float groundDeceleration = 6f;
        }
    }
}
