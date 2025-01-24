using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace Rat
{
    public class MM_MainView : ViewBase
    {
        private Button _levelSelectionButton;
        private Button _howToPlayButton;
        private Button _settingsButton;


        protected override void OnEnable()
        {
            base.OnEnable();

            _levelSelectionButton = m_view.Query<Button>(GC.Views.MainMenu.LEVEL_SELECTION_BUTTON);
            _howToPlayButton = m_view.Query<Button>(GC.Views.MainMenu.HOW_TO_PLAY_BUTTON);
            _settingsButton = m_view.Query<Button>(GC.Views.MainMenu.SETTINGS_BUTTON);
            
            _levelSelectionButton.clicked += LevelSelectionButtonOnclicked; 
            _howToPlayButton.clicked += HowToPlayButtonOnclicked;
            _settingsButton.clicked += SettingsButtonOnclicked;
        }
        
        private void OnDisable()
        {
            _levelSelectionButton.clicked -= LevelSelectionButtonOnclicked; 
            _howToPlayButton.clicked -= HowToPlayButtonOnclicked;
            _settingsButton.clicked -= SettingsButtonOnclicked;
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
        
        private void SettingsButtonOnclicked()
        {
            Debug.Log("Credits clicked");
            GameEventsView.MainMenu.OnPressSettings?.Invoke();
        }
    }
}