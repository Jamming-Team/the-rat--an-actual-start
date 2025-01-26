using UnityEngine;

namespace Rat
{
    public class SlowZone : MonoBehaviour
    {
        [SerializeField]
        private LayerMask _playerLayerMask;
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if ((_playerLayerMask.value & (1 << other.gameObject.layer)) != 0)
            {
                other.gameObject.GetComponent<Player>().GetPlayerController().ApplySlowZone(true);
            }
        }
        
        private void OnTriggerExit2D(Collider2D other)
        {
            if ((_playerLayerMask.value & (1 << other.gameObject.layer)) != 0)
            {
                other.gameObject.GetComponent<Player>().GetPlayerController().ApplySlowZone(false);
            }
        }
    }
}