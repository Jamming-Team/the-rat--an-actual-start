using UnityEngine;
using UnityEngine.UI;

namespace Rat
{
    public class MM_LevelSelectionView : ViewBase
    {
        [SerializeField]
        private Button _playButton;

        protected override void OnEnable()
        {
            base.OnEnable();

            // _playButton.onClick.AddListener(PlayButtonOnclicked); 
        }
        
        private void OnDisable()
        {
            // _playButton.onClick.RemoveListener(PlayButtonOnclicked); 
        }

        private void PlayButtonOnclicked()
        {
            GameEventsView.MainMenu.OnPressPlay?.Invoke();
        }
    }
}