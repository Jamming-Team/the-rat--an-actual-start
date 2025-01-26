using UnityEngine;
using UnityEngine.UI;

namespace Rat
{
    public class MM_SettingsView : ViewBase
    {
        [SerializeField]
        private Button _backButton;
        
        protected override void OnEnable()
        {
            base.OnEnable();
            
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

        public void SetMasterVolume(float volume)
        {
            SoundManager.Instance.mixer.SetFloat("MasterVolume", Mathf.Log10(volume) * 20f);
        }

        public void SetMusicVolume(float volume)
        {
            SoundManager.Instance.mixer.SetFloat("MasterVolume", Mathf.Log10(volume) * 20f);

        }

        public void SetSfxVolume(float volume)
        {
            SoundManager.Instance.mixer.SetFloat("MasterVolume", Mathf.Log10(volume) * 20f);

        }
    }
}