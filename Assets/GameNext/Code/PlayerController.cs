using System;
using UnityEngine;

namespace GameNext
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private PlayerControllerStatsSO _statsSO;
        private PlayerControllerStatsSO.StatsData _stats => _statsSO.data;
        
        [SerializeField] private Collider2D _collider2D;
        [SerializeField] private Rigidbody2D _rigidbody2D;
        
        private FrameInput _frameInput;
        private Vector2 _frameForce;
        private Vector2 _pastVelocity;
        
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
        
        }

        private void FixedUpdate()
        {
            _frameForce = Vector2.zero;
            _pastVelocity = _rigidbody2D.linearVelocity;
            
            HandleX();
        }

        // Update is called once per frame
        void Update()
        {
            GatherInput();
        }

        private void GatherInput()
        {
            _frameInput = new FrameInput
            {
                JumpDown = Input.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.C),
                JumpHeld = Input.GetButton("Jump") || Input.GetKey(KeyCode.C),
                Move = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"))
            };
        }

        private void HandleX()
        {
            if (_frameInput.Move.x == 0)
            {
                if (Mathf.Abs(_rigidbody2D.linearVelocityX) > 0.01)
                    _rigidbody2D.AddForceX(-Mathf.Sign(_pastVelocity.x) * _stats.groundDeceleration);
                else
                {
                    _rigidbody2D.linearVelocityX = 0.0f;
                }
            }
            else
            {
                if (Mathf.Abs(_rigidbody2D.linearVelocityX) < _stats.maxGroundSpeed || _frameInput.Move.x / _pastVelocity.x < 0 )
                    _rigidbody2D.AddForceX(Mathf.Sign(_frameInput.Move.x) * _stats.groundAcceleration);
            }
            

            
            // _rigidbody2D.linearVelocityX
        }

        private void ApplyForce()
        {
            
        }
        
        
        
        public struct FrameInput
        {
            public bool JumpDown;
            public bool JumpHeld;
            public Vector2 Move;
        }
    }
}
