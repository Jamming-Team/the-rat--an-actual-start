using System;
using System.Collections.Generic;
using UnityEngine;
using static GC.MC;

namespace MeatAndSoap
{
    [RequireComponent(typeof(StateMachine))]
    public class MovementController : MonoBehaviour, IVisitableMC<MCStatsData.JumpData>, IPushable
    {
        [SerializeField] 
        private MCDataFillerVisitor _dataFillerVisitor;
        
        [SerializeField] 
        private CollisionsGatherer _collisionsGatherer;

        [SerializeField]
        public MCStatsData.JumpData jumpData;
        
        [HideInInspector]
        public FrameData frameData { get; private set; } = new FrameData();
        private Vector2 _externalBurst = Vector2.zero;
        [HideInInspector]
        public FrameInput frameInput { get; private set; } = new FrameInput();
        [HideInInspector]
        public FrameCollisions frameCollisions { get; private set; } = new FrameCollisions();
        public Dictionary<Conditions, bool> conditions { get; private set; } = new Dictionary<Conditions, bool>();
        public Dictionary<Markers, float> markers { get; private set; } = new Dictionary<Markers, float>();

        private Rigidbody2D _rb;
        private StateMachine _sm;

        private void Awake()
        {
            Debug.Log(1);
        }

        public void Init(Rigidbody2D rb)
        {
            _sm = GetComponent<StateMachine>();
            
            foreach (Conditions condition in Enum.GetValues(typeof(Conditions)))
            {
                conditions[condition] = false;
            }
            foreach (Markers marker in Enum.GetValues(typeof(Markers)))
            {
                markers[marker] = float.MinValue;
            }
            
            _rb = rb;
            
            _dataFillerVisitor.FillAllTheData();
            
            _sm.Init(this);
        }

        private void FixedUpdate()
        {
            frameData.Refresh(_rb);
            
            _collisionsGatherer.Gather(frameCollisions);
            HandleInput();
            
            (_sm.currentState as IPC_States)?.HandleTransition();
            (_sm.currentState as IPC_States)?.HandleModules();
            (_sm.currentState as IPC_States)?.HandleInnerForce();

            ApplyForce();
        }
        
        private void Update()
        {
            HandleInput();
        }

        private void HandleInput()
        {
            conditions[Conditions.HasJumpToConsume] = false;
            
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
                                                         && frameData.time < markers[Markers.TimeLeftGround] + jumpData.coyoteTime)
                                                     && markers[Markers.TimeJumpWasPressed] > 0f; 
        }
        
        private void ApplyForce()
        {
            frameData.frameBurst += _externalBurst;
            _externalBurst = Vector2.zero;
            
            frameData.frameForce *= _rb.mass;
            frameData.frameBurst *= _rb.mass;
            
            if (frameData.frameForce != Vector2.zero)
                _rb.AddForce(frameData.frameForce, ForceMode2D.Force);
            if (frameData.frameBurst != Vector2.zero)
                _rb.AddForce(frameData.frameBurst, ForceMode2D.Impulse);
        }

        public void SupplyInput(Vector2 move, bool jumpPerformed, bool jumpInProgress)
        {
            frameInput.move = move;
            frameInput.jumpDown = jumpPerformed;
            frameInput.jumpHeld = jumpInProgress;
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

        public void FillData(MCStatsData.JumpData data)
        {
            jumpData = data;
        }

        public void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }

        public void Push(Vector2 pushForce)
        {
            _externalBurst += pushForce;
        }
    }
}

