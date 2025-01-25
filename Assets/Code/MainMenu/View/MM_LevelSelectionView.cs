using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Rat
{
    public class MM_LevelSelectionView : ViewBase
    {
        [SerializeField]
        private Button _backButton;
        [SerializeField]
        private LevelButton _levelButtonPrefab;
        [SerializeField] private GridLayoutGroup _gridLayoutGroup;

        private List<LevelButton> _levelsCells = new();
        
        protected override void OnEnable()
        {
            base.OnEnable();
            
            
            _gridLayoutGroup.gameObject.GetComponentsInChildren(_levelsCells);

            foreach (var levelButton in _levelsCells)
            {
                Destroy(levelButton.gameObject);
            }

            int i = 0;
            foreach (var gameLevel in GameManager.Instance.gameLevelsSO.levels)
            {
                var levelButton = Instantiate(_levelButtonPrefab, _gridLayoutGroup.transform);
                levelButton.Initialize(gameLevel, i);
                i++;
            }

            _backButton.onClick.AddListener(BackButtonOnclicked); 
        }
        
        private void OnDisable()
        {
            _backButton.onClick.RemoveListener(BackButtonOnclicked); 
        }

        private void BackButtonOnclicked()
        {
            GameEventsView.MainMenu.OnPressBackButton?.Invoke();
        }
    }
}