using System;
using Rat.Interface;
using UnityEngine;

namespace Rat
{
    /// <summary>
    /// Hey!
    /// Tarodev here. I built this controller as there was a severe lack of quality & free 2D controllers out there.
    /// I have a premium version on Patreon, which has every feature you'd expect from a polished controller. Link: https://www.patreon.com/tarodev
    /// You can play and compete for best times here: https://tarodev.itch.io/extended-ultimate-2d-controller
    /// If you hve any questions or would like to brag about your score, come to discord: https://discord.gg/tarodev
    /// </summary>
    public class PlayerController : MonoBehaviour, IPlayerController, IPushable
    {
        [SerializeField] private ScriptableStats stats;
        private ScriptableStats.StatsData _slowedStatsData;
        public ScriptableStats.StatsData _stats => isInSlowZone ? _slowedStatsData : stats.data;
        
        private Rigidbody2D _rb;
        private CircleCollider2D _col;
        private FrameInput _frameInput;
        private Vector2 _frameVelocity;
        private bool _cachedQueryStartInColliders;
        
        #region Interface

        public Vector2 FrameInput => _frameInput.Move;
        public event Action<bool, float> GroundedChanged;
        public event Action Jumped;
        public bool isActive = false;
        public bool isInSlowZone = false;

        #endregion

        private float _time;
        private Vector2 _externalForceToApply = Vector2.zero;
        private bool _initialized;

        public void Init(Rigidbody2D rb, CircleCollider2D col)
        {
            _rb = rb;
            _col = col;
            
            _cachedQueryStartInColliders = Physics2D.queriesStartInColliders;

            CreateSlowedStats();

            _initialized = true;
        }
        
        private void Awake()
        {
            // _rb = GetComponent<Rigidbody2D>();
            // _col = GetComponent<CapsuleCollider2D>();

            // _cachedQueryStartInColliders = Physics2D.queriesStartInColliders;
        }

        private void CreateSlowedStats()
        {
            _slowedStatsData = stats.data.Clone();
            _slowedStatsData.MaxSpeed *= stats.data.slowScalerX;
            _slowedStatsData.Acceleration *= stats.data.slowScalerX;
            _slowedStatsData.GroundDeceleration *= Mathf.Pow(stats.data.slowScalerX, -1);
            _slowedStatsData.AirDeceleration *= Mathf.Pow(stats.data.slowScalerX, -1);
            _slowedStatsData.MaxFallSpeed *= stats.data.slowScalerY;
            _slowedStatsData.FallAcceleration *= stats.data.slowScalerY;
            
            _slowedStatsData.JumpPower *= Mathf.Clamp(0,1,stats.data.slowScalerY * 2);

        }

        private void Update()
        {
            if (!_initialized) 
                return;
            
            _time += Time.deltaTime;
            if (isActive)
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

            if (_stats.SnapInput)
            {
                _frameInput.Move.x = Mathf.Abs(_frameInput.Move.x) < _stats.HorizontalDeadZoneThreshold ? 0 : Mathf.Sign(_frameInput.Move.x);
                _frameInput.Move.y = Mathf.Abs(_frameInput.Move.y) < _stats.VerticalDeadZoneThreshold ? 0 : Mathf.Sign(_frameInput.Move.y);
            }

            if (_frameInput.JumpDown)
            {
                _jumpToConsume = true;
                _timeJumpWasPressed = _time;
            }
        }

        private void FixedUpdate()
        {
            if (!_initialized)
                return;
            
            CheckCollisions();

            HandleJump();
            HandleDirection();
            HandleGravity();
            
            HandleExternalForce();
            HandleSlowZoneEffect();
            
            
            
            ApplyMovement();
        }
        
        #region Collisions
        
        private float _frameLeftGrounded = float.MinValue;
        private bool _grounded;
        public bool grounded => _grounded;
        
        private bool _sideHit = false;

        private void CheckCollisions()
        {
            Physics2D.queriesStartInColliders = false;

            // Ground and Ceiling
            bool groundHit = Physics2D.CircleCast(_col.bounds.center, _col.radius, Vector2.down, _stats.GrounderDistance, _stats.ObstacleLayers);
            bool ceilingHit = Physics2D.CircleCast(_col.bounds.center, _col.radius, Vector2.up, _stats.GrounderDistance, _stats.ObstacleLayers);

            _sideHit = Physics2D.CircleCast(_col.bounds.center, _col.radius, Vector2.right, _stats.GrounderDistance, _stats.ObstacleLayers)
                       || Physics2D.CircleCast(_col.bounds.center, _col.radius, Vector2.left, _stats.GrounderDistance, _stats.ObstacleLayers);

            // Debug.Log(_sideHit);
            
            // Hit a Ceiling
            if (ceilingHit) _frameVelocity.y = Mathf.Min(0, _frameVelocity.y);

            // Landed on the Ground
            if (!_grounded && groundHit)
            {
                _grounded = true;
                _coyoteUsable = true;
                _bufferedJumpUsable = true;
                _endedJumpEarly = false;
                GroundedChanged?.Invoke(true, Mathf.Abs(_frameVelocity.y));
            }
            // Left the Ground
            else if (_grounded && !groundHit)
            {
                _grounded = false;
                _frameLeftGrounded = _time;
                GroundedChanged?.Invoke(false, 0);
            }

            Physics2D.queriesStartInColliders = _cachedQueryStartInColliders;
        }

        #endregion


        #region Jumping

        private bool _jumpToConsume;
        private bool _bufferedJumpUsable;
        private bool _endedJumpEarly;
        private bool _coyoteUsable;
        private float _timeJumpWasPressed;

        private bool HasBufferedJump => _bufferedJumpUsable && _time < _timeJumpWasPressed + _stats.JumpBuffer;
        private bool CanUseCoyote => _coyoteUsable && !_grounded && _time < _frameLeftGrounded + _stats.CoyoteTime;

        private void HandleJump()
        {
            if (!_endedJumpEarly && !_grounded && !_frameInput.JumpHeld && _rb.linearVelocity.y > 0) _endedJumpEarly = true;

            if (!_jumpToConsume && !HasBufferedJump) return;

            if (_grounded || CanUseCoyote) ExecuteJump();

            _jumpToConsume = false;
        }

        private void ExecuteJump()
        {
            _endedJumpEarly = false;
            _timeJumpWasPressed = 0;
            _bufferedJumpUsable = false;
            _coyoteUsable = false;
            _frameVelocity.y = _stats.JumpPower;
            Jumped?.Invoke();
        }

        #endregion

        #region Horizontal

        private void HandleDirection()
        {
            if (_frameInput.Move.x == 0 || _sideHit && !_grounded && Mathf.Abs(_rb.linearVelocity.x) <= 0.01f)
            {
                var deceleration = _grounded ? _stats.GroundDeceleration : _stats.AirDeceleration;
                _frameVelocity.x = Mathf.MoveTowards(_frameVelocity.x, 0, deceleration * Time.fixedDeltaTime);
            }
            else
            {
                // if (_sideHit && !_grounded && Mathf.Abs(_rb.linearVelocity.x) <= 0.01f)
                //     return;
                _frameVelocity.x = Mathf.MoveTowards(_frameVelocity.x, _frameInput.Move.x * _stats.MaxSpeed, _stats.Acceleration * Time.fixedDeltaTime);
            }
        }

        #endregion

        #region Gravity

        private void HandleGravity()
        {
            if (_grounded && _frameVelocity.y <= 0f)
            {
                _frameVelocity.y = _stats.GroundingForce;
            }
            else
            {
                var inAirGravity = _stats.FallAcceleration;
                if (_endedJumpEarly && _frameVelocity.y > 0) inAirGravity *= _stats.JumpEndEarlyGravityModifier;
                
                _frameVelocity.y = Mathf.MoveTowards(_frameVelocity.y, -_stats.MaxFallSpeed, inAirGravity * Time.fixedDeltaTime);
            }
        }

        #endregion

        private void ApplyMovement() => _rb.linearVelocity = _frameVelocity;

#if UNITY_EDITOR
        private void OnValidate()
        {
            if (_stats == null) Debug.LogWarning("Please assign a ScriptableStats asset to the Player Controller's Stats slot", this);
        }
#endif
        public void ApplyForce(Vector2 forceVector)
        {
            var newForceVector = Vector2.Lerp(forceVector, Vector2.up, _stats.upDirRatio);
            var finalForce = newForceVector;
            finalForce.Scale(new Vector2(_stats.pushScaleX, _stats.pushScaleY));
            var bumpedForce = Vector2.ClampMagnitude(newForceVector * -_frameVelocity * _stats.pushBackForce, _stats.pushBackForceMax);
            finalForce += bumpedForce;
            _externalForceToApply += finalForce;
        }
        
        public void HandleExternalForce()
        {
            _frameVelocity += _externalForceToApply;
            _externalForceToApply = Vector2.zero;
        }

        public void ApplySlowZone(bool entered)
        {
            isInSlowZone = entered;
        }

        private void HandleSlowZoneEffect()
        {
            if (isInSlowZone)
            {
                if (_frameVelocity.magnitude > _stats.slowedZoneMaxSpeed)
                {
                    _frameVelocity = Vector2.MoveTowards(_frameVelocity, _frameVelocity.normalized * _stats.slowedZoneMaxSpeed,  _stats.slowedZoneDownAcceleration * Time.fixedDeltaTime);
                };
                // _frameVelocity *= new Vector2(_stats.slowScalerX, _stats.slowScalerY);
            }
        }


    }

    public struct FrameInput
    {
        public bool JumpDown;
        public bool JumpHeld;
        public Vector2 Move;
    }

    public interface IPlayerController
    {
        public event Action<bool, float> GroundedChanged;

        public event Action Jumped;
        public Vector2 FrameInput { get; }
    }
}