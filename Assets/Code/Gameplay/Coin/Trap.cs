using UnityEngine;

namespace Rat
{
    public class Trap : MonoBehaviour
    {
        [SerializeField]
        private LayerMask _playerLayerMask;
        
        [SerializeField] private bool _isInBubble = false;


        private void Awake()
        {

            
            var bubble = transform.parent.GetComponent<Bubble>();
            
            if (bubble != null)
            {
                _isInBubble = true;
                bubble.OnBubbleDestroyedWithPlayer += OnBubbleDestroyed;
            }
            
        }
        
        private void OnBubbleDestroyed(Player player)
        {
            GameEvents.OnDeathEvent?.Invoke(player);
            Destroy(gameObject);
        }
    }
}