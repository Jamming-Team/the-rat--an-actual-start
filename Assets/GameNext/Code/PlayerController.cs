using System;
using UnityEngine;

namespace GameNext
{
    [RequireComponent(typeof(Collider2D))]
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private PlayerControllerStatsSO _statsSO;
        private PlayerControllerStatsSO.StatsData _stats => _statsSO.data;
        
        [SerializeField] private Collider2D _collider2D;
        [SerializeField] private Rigidbody2D _rigidbody2D;
        
        public Conditions conditions;

        private StateMachine _stateMachine;
        
        private FrameInput _frameInput;
        private Vector2 _frameForce;
        private Vector2 _pastVelocity;
        
        
        
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
        
        }

        public void Init()
        {
            conditions = new Conditions(this);
        }

        private void FixedUpdate()
        {
            _frameForce = Vector2.zero;
            _pastVelocity = _rigidbody2D.linearVelocity;
            
            
            (_stateMachine.currentState as IPC_States)?.HandleX();
            (_stateMachine.currentState as IPC_States)?.HandleY();

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
                jumpDown = Input.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.C),
                jumpHeld = Input.GetButton("Jump") || Input.GetKey(KeyCode.C),
                move = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"))
            };
        }

        private void GatherCollisions()
        {
            
        }

        private void HandleX()
        {
            if (_frameInput.move.x == 0)
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
                if (Mathf.Abs(_rigidbody2D.linearVelocityX) < _stats.maxGroundSpeed || _frameInput.move.x / _pastVelocity.x < 0 )
                    _rigidbody2D.AddForceX(Mathf.Sign(_frameInput.move.x) * _stats.groundAcceleration);
            }
            

            
            // _rigidbody2D.linearVelocityX
        }

        private void ApplyForce()
        {
            
        }
        
        
        
        public struct FrameInput
        {
            public bool jumpDown;
            public bool jumpHeld;
            public Vector2 move;
        }

        public class Markers
        {
            
        }

        public class Conditions
        {
            private PlayerController _playerController;

            public Conditions(PlayerController playerController)
            {
                _playerController = playerController;
            }
            
            // 
            public bool grounded;
            public bool endedJumpEarly;
            public bool jumpToConsume;
            
            // Complex
            public bool coyoteUsable;
            public bool bufferedJumpUsable;
        }
    }
}
