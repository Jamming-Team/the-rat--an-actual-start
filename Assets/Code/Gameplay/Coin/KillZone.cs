using UnityEngine;

namespace Rat
{
    public class KillZone : MonoBehaviour
    {
        [SerializeField]
        private LayerMask _playerLayerMask;
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if ((_playerLayerMask.value & (1 << other.gameObject.layer)) != 0)
            {
                other.GetComponent<Player>().Kill();
                // Destroy(gameObject);
            }
        }
    }
}