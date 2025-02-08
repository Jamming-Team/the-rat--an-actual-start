using UnityEngine;

namespace MeatAndSoap
{
    public class Bubble : MonoBehaviour
    {
        [SerializeField]
        private LayerMask _playerLayerMask;
        [SerializeField]
        private float _pushForce = 10f;
        
        private float _bumpTimer = 0f;
        private float _bumpTimerMax = 0.1f;
        
        private void Update()
        {
            // Debug.Log(_bubbleGraphicsController);
            _bumpTimer -= Time.deltaTime;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (_bumpTimer > 0f)
                return;

            Debug.Log(collision.gameObject.name);
            
            if ((_playerLayerMask.value & (1 << collision.gameObject.layer)) != 0)
            {
                _bumpTimer = _bumpTimerMax;
                var pushVector = -collision.contacts[0].normal;
                
                
                collision.gameObject.GetComponent<Player>().movementController.Push(pushVector * _pushForce);
            }

        }
    }
}