using System;
using GameNext.GameNext.Code.SM.Gameplay.PC;
using UnityEngine;
using UnityEngine.Serialization;

namespace GameNext
{
    [RequireComponent(typeof(CircleCollider2D))]
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private PlayerControllerStatsSO _statsSO;
        public PlayerControllerStatsSO.StatsData stats => _statsSO.data;
        
        [SerializeField] private CircleCollider2D _col;
        [SerializeField] private Rigidbody2D _rb;
        [SerializeField] private StateMachine _stateMachine;
        
        public FrameInput frameInput;
        public Markers markers;
        public Conditions conditions;
        
        [HideInInspector]
        public Vector2 frameForce;
        [HideInInspector]
        public Vector2 frameBurst;
        [HideInInspector]
        public Vector2 pastVelocity;
        [HideInInspector] 
        public float time;

        
        void Start()
        {
            Init();
        }

        public void Init()
        {
            markers = new Markers();
            conditions = new Conditions(this);
            
            _stateMachine.Init(this);
            time = Time.time;
        }

        private void FixedUpdate()
        {
            frameForce = Vector2.zero;
            frameBurst = Vector2.zero;
            pastVelocity = _rb.linearVelocity;

            GatherCollisions();

            (_stateMachine.currentState as IPC_States)?.HandleTransition();
            (_stateMachine.currentState as IPC_States)?.HandleX();
            (_stateMachine.currentState as IPC_States)?.HandleY();

            ApplyForce();
            
            conditions.hasJumpToConsume = false;
            
            // Debug.Log(conditions.jumpToConsume);
        }

        // Update is called once per frame
        void Update()
        {
            time += Time.deltaTime;
            
            GatherInput();
        }

        private void GatherInput()
        {
            frameInput = new FrameInput
            {
                jumpDown = Input.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.C),
                jumpHeld = Input.GetButton("Jump") || Input.GetKey(KeyCode.C),
                move = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"))
            };
            
            if (frameInput.jumpDown)
            {
                conditions.hasJumpToConsume = true;
                markers.timeJumpWasPressed = time;
            }
        }

        private void GatherCollisions()
        {
            conditions.groundHit = Physics2D.CircleCast(_col.bounds.center, _col.radius, Vector2.down, stats.general.collisionDetectionDistance, stats.general.groundLayer);
            
            conditions.cellingHit = Physics2D.CircleCast(_col.bounds.center, _col.radius, Vector2.up, stats.general.collisionDetectionDistance, stats.general.groundLayer);
            
            conditions.leftHit = Physics2D.CircleCast(_col.bounds.center, _col.radius, Vector2.left, stats.general.collisionDetectionDistance, stats.general.groundLayer);
            
            conditions.rightHit = Physics2D.CircleCast(_col.bounds.center, _col.radius, Vector2.right, stats.general.collisionDetectionDistance, stats.general.groundLayer);
        }

        private void ApplyForce()
        {
            if (frameForce != Vector2.zero)
                _rb.AddForce(frameForce, ForceMode2D.Force);
            if (frameBurst != Vector2.zero)
                _rb.AddForce(frameBurst, ForceMode2D.Impulse);
        }

        public void NullifyVelocity(bool x = false, bool y = false)
        {
            var newVelocity = _rb.linearVelocity;
            if (x)
                newVelocity.x = 0f;
            if (y)
                newVelocity.y = 0f;
            _rb.linearVelocity = newVelocity;
        }
        
        public struct FrameInput
        {
            public bool jumpDown;
            public bool jumpHeld;
            public Vector2 move;
        }

        public class Markers
        {
            public float timeJumpWasPressed = float.MinValue;
            public float timeLeftGround = float.MinValue;
        }

        public class Conditions
        {
            private PlayerController _pc;

            public Conditions(PlayerController pc)
            {
                _pc = pc;
            }
            
            // Physics
            public bool groundHit;
            public bool cellingHit;
            public bool leftHit;
            public bool rightHit;
            
            // Simple
            public bool endedJumpEarly;
            public bool hasJumpToConsume;
            public bool coyoteUsable;
            // public bool bufferedJumpUsable;
            
            // Complex
            public bool grounded => _pc._stateMachine.currentState.GetType() == typeof(Grounded);
            // public bool canUseCoyote => coyoteUsable 
            //                             && _pc.time < _pc.markers.timeLeftGround + _pc.stats.inAir.coyoteTime;
            // public bool hasBufferedJump => bufferedJumpUsable 
            //                                && _pc.time < _pc.markers.timeJumpWasPressed + _pc.stats.grounded.jumpBufferTime;
            public bool shouldJump
            {
                get
                {
                    switch (_pc._stateMachine.currentState)
                    {
                        case Grounded:
                            return hasJumpToConsume
                            || _pc.time < _pc.markers.timeJumpWasPressed + _pc.stats.grounded.jumpBufferTime; 
                        case InAir:
                            return hasJumpToConsume
                                   && (coyoteUsable
                                       && _pc.time < _pc.markers.timeLeftGround + _pc.stats.inAir.coyoteTime); 
                    }
                    return false;
                }
            }

            public bool antiInputX =>
                _pc.frameInput.move.x / _pc.pastVelocity.x < 0;
        }
    }
}
