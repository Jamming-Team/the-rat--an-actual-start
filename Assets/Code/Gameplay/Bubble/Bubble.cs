using UnityEngine;

namespace Rat
{
    public class Bubble : MonoBehaviour
    {
        [SerializeField]
        private bool _isDestructible = true;
        [SerializeField]
        private int amount;
        // [SerializeField]
        // private float _pushScaleX = 30;
        // [SerializeField]
        // private float _pushScaleY = 40;
        [SerializeField]
        private LayerMask _playerLayerMask;
        // [SerializeField] [Range(0, 1)]
        // private float _upDirRatio = 0.5f;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (( _playerLayerMask.value & (1 << collision.gameObject.layer)) != 0)
            {
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
                if (amount != 0)
                {
                    GameEvents.OnCoinCollected?.Invoke(amount);
                }
                if (_isDestructible)
                    StartAnimationAndDestroy();
            }
        }

        private void StartAnimationAndDestroy()
        {
            // TODO: start animation
            Destroy(gameObject);
        }
    }
}