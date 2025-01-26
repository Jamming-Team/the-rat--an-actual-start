using UnityEngine;

namespace Rat
{
    public class FinishZone : MonoBehaviour
    {
        [SerializeField]
        private LayerMask _playerLayerMask;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if ((_playerLayerMask.value & (1 << other.gameObject.layer)) != 0)
            {
                GameEvents.OnEnteredFinish?.Invoke();
                // Destroy(gameObject);
            }
        }
    }
}