using UnityEngine;
using UnityEngine.UIElements;

namespace Rat
{
    public class MM_LevelSelectionView : ViewBase
    {
        
        private Button _playButton;

        protected override void OnEnable()
        {
            base.OnEnable();

            _playButton = m_view.Query<Button>(GC.Views.MainMenu.PLAY_BUTTON);
            
            _playButton.clicked += PlayButtonOnclicked; 
        }
        
        private void OnDisable()
        {
            _playButton.clicked += PlayButtonOnclicked; 
        }

        private void PlayButtonOnclicked()
        {
            GameEventsView.MainMenu.OnPressPlay?.Invoke();
        }
    }
}