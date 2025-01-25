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
        private CapsuleCollider2D _capsuleCollider;


        public void Init()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _capsuleCollider = GetComponent<CapsuleCollider2D>();
            
            _playerController.isActive = true;
            _graphicsController.Init();
            _playerController.Init(_rigidbody, _capsuleCollider);
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