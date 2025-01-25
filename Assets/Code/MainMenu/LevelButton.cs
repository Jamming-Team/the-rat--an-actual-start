using TMPro;
using UnityEngine.UI;
using UnityEngine;

namespace Rat
{
    public class LevelButton : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text _levelNameLabel;
        [SerializeField]
        private TMP_Text _scoreLabel;

        private int _levelIndex;


        public void Initialize(GameLevelData gameLevelData, int levelIndex)
        {
            _levelIndex = levelIndex;

            _levelNameLabel.text = gameLevelData.name;
            _scoreLabel.text = $"score: {gameLevelData.playerScore} / {gameLevelData.maxScore}";

        }

        public void OnLevelButtonClick()
        {
            GameEventsView.MainMenu.OnPressLevelButton?.Invoke(_levelIndex);
        }
    }
}