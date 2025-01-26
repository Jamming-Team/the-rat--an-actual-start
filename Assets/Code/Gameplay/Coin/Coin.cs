using System;
using UnityEngine;

namespace Rat
{
    public class Coin : MonoBehaviour
    {
        
        [SerializeField] private SoundData _coinSound;
        
        [SerializeField]
        private int amount = 1;
        [SerializeField]
        private LayerMask _playerLayerMask;
        
        [SerializeField] private bool _isInBubble = false;


        private void Awake()
        {
            var data = GameManager.Instance.persistentLevelData;
            if (data.shouldPersist && data.levelName == GameManager.Instance.currentLevelData.name)
            {
                if (data.coinsList.Contains(gameObject.name))
                    Destroy(gameObject);
            }
            
            var bubble = transform.parent.GetComponent<Bubble>();
            
            if (bubble != null)
            {
                _isInBubble = true;
                bubble.OnBubbleDestroyed += OnBubbleDestroyed;
            }
        }

        private void OnDeleteCoin(string obj)
        {
            if (gameObject.name == obj)
                Destroy(gameObject);
        }

        private void OnBubbleDestroyed()
        {
            GameEvents.OnCoinCollected?.Invoke(amount);
            GameEvents.OnCoinCollectedPersist?.Invoke(gameObject.name);
            SoundManager.Instance.CreateSoundBuilder()
                .WithRandomPitch()
                .WithPosition(transform.position)
                .Play(_coinSound);
            Destroy(gameObject);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if ((_playerLayerMask.value & (1 << other.gameObject.layer)) != 0)
            {
                GameEvents.OnCoinCollected?.Invoke(amount);
                GameEvents.OnCoinCollectedPersist?.Invoke(gameObject.name);
                SoundManager.Instance.CreateSoundBuilder()
                    .WithRandomPitch()
                    .WithPosition(transform.position)
                    .Play(_coinSound);
                Destroy(gameObject);
            }
        }
        
        
    }
}
