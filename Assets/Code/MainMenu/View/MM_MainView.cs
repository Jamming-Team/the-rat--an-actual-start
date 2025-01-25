using System;
using UnityEngine;
// using UnityEngine.UIElements;
using UnityEngine.UI;

namespace Rat
{
    public class MM_MainView : ViewBase
    {
        [SerializeField]
        private Button _levelSelectionButton;
        [SerializeField]
        private Button _settingsButton;


        protected override void OnEnable()
        {
            base.OnEnable();
            _levelSelectionButton.onClick.AddListener(LevelSelectionButtonOnclicked); 
            _settingsButton.onClick.AddListener(SettingsButtonOnclicked);
            Debug.Log("Enabled");


        }
        
        private void OnDisable()
        {
            _levelSelectionButton.onClick.RemoveListener(LevelSelectionButtonOnclicked); 
            _settingsButton.onClick.RemoveListener(SettingsButtonOnclicked);
        }

        private void LevelSelectionButtonOnclicked()
        {
            GameEventsView.MainMenu.OnPressLevelSelection?.Invoke();
        }
        
        private void HowToPlayButtonOnclicked()
        {
            // Debug.Log("CLicked");
            GameEventsView.MainMenu.OnPressHowToPlay?.Invoke();
        }
        
        public void SettingsButtonOnclicked()
        {
            // Debug.Log("Credits clicked");
            GameEventsView.MainMenu.OnPressSettings?.Invoke();
        }
    }
}