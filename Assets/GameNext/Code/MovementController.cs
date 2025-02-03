using System;
using System.Collections.Generic;
using UnityEngine;
using static GC.MC;

namespace GameNext
{
    [RequireComponent(typeof(Collider2D)), RequireComponent(typeof(StateMachine))]
    public class MovementController : MonoBehaviour
    {
        [SerializeField] 
        private CollisionsGatherer _collisionsGatherer;

        [SerializeField]
        public MCStatsData.JumpData jumpData = new MCStatsData.JumpData();
        
        [HideInInspector]
        public FrameData frameData { get; private set; } = new FrameData();
        [HideInInspector]
        public FrameInput frameInput { get; private set; } = new FrameInput();
        [HideInInspector]
        public FrameCollisions frameCollisions { get; private set; } = new FrameCollisions();
        public Dictionary<Conditions, bool> conditions { get; private set; } = new Dictionary<Conditions, bool>();
        public Dictionary<Markers, float> markers { get; private set; } = new Dictionary<Markers, float>();

        private Rigidbody2D _rb;
        private Collider2D _col;
        private StateMachine _sm;

        private void Awake()
        {
            _col = GetComponent<Collider2D>();
            _sm = GetComponent<StateMachine>();
            
            foreach (Conditions condition in Enum.GetValues(typeof(Conditions)))
            {
                conditions[condition] = false;
            }
            foreach (Markers marker in Enum.GetValues(typeof(Markers)))
            {
                markers[marker] = float.MinValue;
            }
        }

        public void Init(Rigidbody2D rb)
        {
            _rb = rb;
            _sm.Init(this);
        }

        private void FixedUpdate()
        {
            frameData.Refresh(_rb);
            
            _collisionsGatherer.Gather(_col, frameCollisions);
            
            
            (_sm.currentState as IPC_States)?.HandleTransition();
            (_sm.currentState as IPC_States)?.HandleModules();
            (_sm.currentState as IPC_States)?.HandleInnerForce();

            ApplyForce();
        }
        
        private void Update()
        {
            GatherInput();
        }

        private void GatherInput()
        {
            frameInput.jumpDown = Input.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.C);
            frameInput.jumpHeld = Input.GetButton("Jump") || Input.GetKey(KeyCode.C);
            frameInput.move = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            
            if (frameInput.jumpDown)
            {
                conditions[Conditions.HasJumpToConsume] = true;
                markers[Markers.TimeJumpWasPressed] = frameData.time;
            }
            
            conditions[Conditions.AntiInputX] = Mathf.Sign(frameInput.move.x) / Mathf.Sign(frameData.pastVelocity.x) < 0;
            
            conditions[Conditions.ShouldJumpGrounded] = conditions[Conditions.HasJumpToConsume]
                                                        || frameData.time < markers[Markers.TimeJumpWasPressed] + jumpData.jumpBufferTime; 
            conditions[Conditions.ShouldJumpInAir] = conditions[Conditions.HasJumpToConsume]
                                                     && (conditions[Conditions.CoyoteUsable]
                                                         && frameData.time < markers[Markers.TimeLeftGround] + jumpData.coyoteTime); 
        }
        
        private void ApplyForce()
        {
            if (frameData.frameForce != Vector2.zero)
                _rb.AddForce(frameData.frameForce, ForceMode2D.Force);
            if (frameData.frameBurst != Vector2.zero)
                _rb.AddForce(frameData.frameBurst, ForceMode2D.Impulse);
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
        
        // Inner Classes
        
        public class FrameData
        {
            public Vector2 frameForce;
            public Vector2 frameBurst;
            public Vector2 pastVelocity;
            public float time;

            public void Refresh(Rigidbody2D rb)
            {
                frameForce = Vector2.zero;
                frameBurst = Vector2.zero;
                pastVelocity = rb.linearVelocity;
                time += Time.fixedDeltaTime;
            }
        }

        public class FrameInput
        {
            public bool jumpDown;
            public bool jumpHeld;
            public Vector2 move;
        }

        public class FrameCollisions
        {
            public bool groundHit;
            public bool cellingHit;
            public bool leftHit;
            public bool rightHit;
        }
    }
}