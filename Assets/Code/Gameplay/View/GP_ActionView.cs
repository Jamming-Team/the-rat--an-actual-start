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
        [SerializeField]
        private TMP_Text _respawnPointPriceText;

        [SerializeField] private Image _barImage;

        private void Awake()
        {
            _barImage.fillAmount = 0f;
            _respawnPointPriceText.text = GameManager.Instance.persistentLevelData.currentPrice.ToString();
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            _respawnPointPriceText.text = GameManager.Instance.persistentLevelData.currentPrice.ToString();
            _pauseButton.onClick.AddListener(PauseButtonOnclicked); 
            GameEventsView.Gameplay.OnScoreChanged += OnScoreChanged;
            GameEventsView.Gameplay.OnPriceChanged += OnPriceChanged;
            GameEventsView.Gameplay.OnHoldSpawn += OnHoldSpawn;
        }

        private void OnDisable()
        {
            _pauseButton.onClick.RemoveListener(PauseButtonOnclicked); 
            GameEventsView.Gameplay.OnScoreChanged -= OnScoreChanged;
            GameEventsView.Gameplay.OnPriceChanged -= OnPriceChanged;
            GameEventsView.Gameplay.OnHoldSpawn -= OnHoldSpawn;
        }

        private void PauseButtonOnclicked()
        {
            GameEventsView.Gameplay.OnPressPause?.Invoke();
        }
        
        private void OnScoreChanged(int obj)
        {
            _scoreValueText.text = obj.ToString();
        }
        
        private void OnPriceChanged(int obj)
        {
            _respawnPointPriceText.text = obj.ToString();
        }
        
        private void OnHoldSpawn(float obj)
        {
            _barImage.fillAmount = obj;
        }

    }
}