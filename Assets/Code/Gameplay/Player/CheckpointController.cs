using System;
using UnityEngine;

namespace Rat
{
    public class CheckpointController : MonoBehaviour
    {
        private PlayerController _playerController;

        [SerializeField] private int _price;
        [SerializeField] private GameObject _checkpointPrefab;

        [SerializeField] private float _timeNeededToFill = 1f;
        private float _fillSpeed => 1 / _timeNeededToFill;
        private float _fiilRate = 0f;

        private bool _onHold = false;
        
        public void Init(PlayerController playerController)
        {
            GameInputManager.Instance.player.interact += Interact;
            _playerController = playerController;
        }

        private void Update()
        {
            if (_onHold && _playerController.grounded)
            {
                _fiilRate = Mathf.Clamp(_fiilRate + Time.deltaTime * _fillSpeed, 0f, 1f);
                if (_fiilRate == 1f)
                {
                    _onHold = false;
                    MakeSpawnPoint();
                }
            }
            else
            {
                _fiilRate = Mathf.Clamp( _fiilRate - Time.deltaTime * _fillSpeed, 0f, 1f);
            }

            if (Time.frameCount % 10 == 0)
            {
                GameEventsView.Gameplay.OnHoldSpawn?.Invoke(_fiilRate);
            }
        }

        private void OnDestroy()
        {
            if (GameInputManager.Instance != null)
                GameInputManager.Instance.player.interact -= Interact;
        }

        private void Interact(bool isActive)
        {
            Debug.Log($"price: {GameManager.Instance.persistentLevelData.currentPrice}, Score: {GMC_Gameplay.Instance.currentScore}");
            if (GameManager.Instance.persistentLevelData.currentPrice > GMC_Gameplay.Instance.currentScore)
            {
                return;
            }
            
            _onHold = isActive;
            
            }

        private void MakeSpawnPoint()
        {
            var checkpoint = Instantiate(_checkpointPrefab, transform.position + new Vector3(0,1,0), Quaternion.identity);
            
            GMC_Gameplay.Instance.SetCurrentScore(GMC_Gameplay.Instance.currentScore - GameManager.Instance.persistentLevelData.currentPrice);
            GameEvents.OnSaveLocation?.Invoke(checkpoint.transform.position, GMC_Gameplay.Instance.currentScore, gameObject.name);
            
        }
    }
}