using System;
using UnityEngine;

namespace Rat
{
    public class Bubble : MonoBehaviour
    {
        public Action OnBubbleDestroyed;
        
        [SerializeField]
        private bool _isDestructible = true;

        [SerializeField]
        private int _endurance = 1;
        private int _maxEndurance;
        [SerializeField]
        private SpriteRenderer _enduranceMaskSpriteRenderer;
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
                _maxEndurance = _endurance;
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
                {
                    _endurance--;
                    if (_endurance <= 0)
                    {
                        StartAnimationAndDestroy();
                    }
                    else
                    {
                        var color = _enduranceMaskSpriteRenderer.color;
                        color.a = 1f * ((_endurance - 1f) / (_maxEndurance - 1f));
                        _enduranceMaskSpriteRenderer.color = color;
                    }
                }
            }
        }

        private void StartAnimationAndDestroy()
        {
            // TODO: start animation
            OnBubbleDestroyed?.Invoke();
            GameEvents.OnBubbleDestroyedPersist?.Invoke(gameObject.name);
            Destroy(gameObject);
        }
    }
}