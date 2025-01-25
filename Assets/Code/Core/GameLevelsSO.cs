using UnityEngine;
using System.Collections.Generic;

namespace Rat
{
    [CreateAssetMenu(fileName = "GameLevelsSO", menuName = "Rat/GameLevelsSO")]
    public class GameLevelsSO : ScriptableObject
    {
        public List<GameLevelData> levels;
    }
    
    [System.Serializable]
    public class GameLevelData
    {
        public string name;
        public int playerScore;
        public int maxScore;
        public OverlookCameraData overlookCamera;
        
        public GameObject level;
        
        [System.Serializable]
        public class OverlookCameraData
        {
            public Vector2 position;
            public float orthographicSize;
        }
        
    }
}