using UnityEngine;

namespace Rat
{
    public class Player : MonoBehaviour
    {
        [SerializeField]
        private PlayerController _playerController;
        
        [SerializeField]
        private GraphicsController _graphicsController;
        private Rigidbody2D _rigidbody;
        private CircleCollider2D _ciclreCollider;


        public void Init()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _ciclreCollider = GetComponent<CircleCollider2D>();
            
            _playerController.isActive = true;
            _graphicsController.Init(_rigidbody);
            _playerController.Init(_rigidbody, _ciclreCollider);
        }

        public void Deactivate()
        {
            _playerController.isActive = false;
        }

        public PlayerController GetPlayerController()
        {
            return _playerController;
        }
    }
}