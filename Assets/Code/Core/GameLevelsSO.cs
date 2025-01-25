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

        public GameObject level;
    }
}