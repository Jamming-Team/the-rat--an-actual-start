using System;
using TMPro;
using UnityEngine;
// using UnityEngine.UIElements;
using UnityEngine.UI;

namespace Rat
{
    public class GP_ActionView : ViewBase
    {
        [SerializeField]
        private Button _pauseButton;
        [SerializeField]
        private TMP_Text _scoreValueText;


        protected override void OnEnable()
        {
            base.OnEnable();
            _pauseButton.onClick.AddListener(PauseButtonOnclicked); 
            GameEventsView.Gameplay.OnScoreChanged += OnScoreChanged;
        }

        private void OnDisable()
        {
            _pauseButton.onClick.RemoveListener(PauseButtonOnclicked); 
            GameEventsView.Gameplay.OnScoreChanged -= OnScoreChanged;
        }

        private void PauseButtonOnclicked()
        {
            GameEventsView.Gameplay.OnPressPause?.Invoke();
        }
        
        private void OnScoreChanged(int obj)
        {
            _scoreValueText.text = obj.ToString();
        }

    }
}