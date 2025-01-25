using System;
using TMPro;
using UnityEngine;
// using UnityEngine.UIElements;
using UnityEngine.UI;

namespace Rat
{
    public class GP_PauseView : ViewBase
    {
        [SerializeField]
        private TMP_Text _levelNameLabel;
        [SerializeField]
        private Button _continueButton;
        [SerializeField]
        private Button _restartButton;
        [SerializeField]
        private Button _toMainMenuButton;


        protected override void OnEnable()
        {
            base.OnEnable();
            _levelNameLabel.text = GameManager.Instance.currentLevelData.name;
            _continueButton.onClick.AddListener(ContinueButtonOnclicked); 
            _restartButton.onClick.AddListener(RestartButtonOnclicked); 
            _toMainMenuButton.onClick.AddListener(ToMainMenuButtonOnclicked); 
        }
        
        private void OnDisable()
        {
            _continueButton.onClick.RemoveListener(ContinueButtonOnclicked); 
            _restartButton.onClick.RemoveListener(RestartButtonOnclicked); 
            _toMainMenuButton.onClick.RemoveListener(ToMainMenuButtonOnclicked); 
        }

        private void ContinueButtonOnclicked()
        {
            GameEventsView.Gameplay.OnPressContinue?.Invoke();
        }
        
        private void RestartButtonOnclicked()
        {
            // Debug.Log("CLicked");
            GameEventsView.Gameplay.OnPressRestart?.Invoke();
        }
        
        public void ToMainMenuButtonOnclicked()
        {
            // Debug.Log("Credits clicked");
            GameEventsView.Gameplay.OnPressToMainMenu?.Invoke();
        }
    }
}