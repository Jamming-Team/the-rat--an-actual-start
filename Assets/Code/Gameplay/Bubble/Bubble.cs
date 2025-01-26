﻿using System;
using UnityEngine;

namespace Rat
{
    public class Bubble : MonoBehaviour
    {
        public Action OnBubbleDestroyed;
        public Action<Player> OnBubbleDestroyedWithPlayer;
        
        [SerializeField]
        private bool _isDestructible = true;

        // [SerializeField]
        // private float _pushScaleX = 30;
        // [SerializeField]
        // private float _pushScaleY = 40;
        [SerializeField]
        private LayerMask _playerLayerMask;
        // [SerializeField] [Range(0, 1)]
        // private float _upDirRatio = 0.5f;
        private float _bumpTimer = 0f;
        private float _bumpTimerMax = 0.1f;

        private void Awake()
        {
            if (_isDestructible)
            {
                var data = GameManager.Instance.persistentLevelData;
                if (data.shouldPersist && data.levelName == GameManager.Instance.currentLevelData.name)
                {
                    if (data.bubblesList.Contains(gameObject.name))
                        Destroy(gameObject);
                }
            }
        }

        private void Update()
        {
            _bumpTimer -= Time.deltaTime;
        }
        
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (_bumpTimer > 0f)
                return;
            
            if (( _playerLayerMask.value & (1 << collision.gameObject.layer)) != 0)
            {
                _bumpTimer = _bumpTimerMax;
                var pushVector = -collision.contacts[0].normal;
                // Debug.Log( pushVector);
                //
                // if (pushVector.y > 0)
                // {
                //     pushVector = Vector2.Lerp(pushVector, Vector2.up, _upDirRatio);
                // }
                // Debug.Log( pushVector);
                //
                // pushVector.Scale(new Vector2(_pushScaleX, _pushScaleY));
                collision.gameObject.GetComponent<Player>().GetPlayerController().ApplyForce(pushVector);

                
                if (_isDestructible)
                    StartAnimationAndDestroy(collision.gameObject.GetComponent<Player>());
            }
        }

        private void StartAnimationAndDestroy(Player player)
        {
            // TODO: start animation
            OnBubbleDestroyed?.Invoke();
            OnBubbleDestroyedWithPlayer?.Invoke(player);
            GameEvents.OnBubbleDestroyedPersist?.Invoke(gameObject.name);
            Destroy(gameObject);
        }
    }
}