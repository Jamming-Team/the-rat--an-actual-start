using UnityEngine;

namespace Rat
{
    public class Coin : MonoBehaviour
    {
        [SerializeField]
        private int amount = 1;
        [SerializeField]
        private LayerMask _playerLayerMask;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if ((_playerLayerMask.value & (1 << other.gameObject.layer)) != 0)
            {
                GameEvents.OnCoinCollected?.Invoke(amount);
                Destroy(gameObject);
            }
        }
    }
}
