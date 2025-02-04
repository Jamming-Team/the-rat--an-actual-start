using EditorAttributes;
using UnityEngine;
using UnityEngine.Serialization;

namespace GameNext
{
    public class NonLinearVelocity : MCModuleCommand<MCStatsData.XMovement>
    {
        private MCStatsData.NonLinearXAcceleration xAcceleration;

        private float _time = 0f;
        private float _factorR = 0f;
        private float _factorL = 0f;
        private float _factor = 0f;

        private float _pastPastVelocity = 0f;
        
        public override void Execute()
        {
            switch (_mc.frameInput.move.x)
            {
                case 0:
                    _factorL = _factorR = _factor = 0f;
                    break;
                case < 0:
                    _factor = _factorL = Mathf.Clamp(_factorL + Time.fixedDeltaTime / xAcceleration.timeTillFullVelocity, 0f, 1f);
                    _factorR =  Mathf.InverseLerp(0f, _stats.maxSpeed, _mc.frameData.pastVelocity.x);
                    break;
                case > 0:
                    _factorL = Mathf.InverseLerp(0f, -_stats.maxSpeed, _mc.frameData.pastVelocity.x);
                    _factor = _factorR =  Mathf.Clamp(_factorR + Time.fixedDeltaTime / xAcceleration.timeTillFullVelocity, 0f, 1f);
                    break;
            }

            _factor *= xAcceleration.timeTillFullVelocity;
            // _stats.acceleration *= EasingLib.DerEaseOutQuad(_factor, k);
            _stats.acceleration *= 1 / xAcceleration.timeTillFullVelocity;

            
            //
            //
            // if (_pastPastVelocity * _frameData.pastVelocity.x < 0)
            //     _factor = 0f;
            // if (_playerInput.move.x != 0f && _playerInput.move.x / Mathf.Sign(_frameData.pastVelocity.x) > 0f)
            // {
            //     _factor += Time.fixedDeltaTime / _timeTillFullAcceleration;
            // }
            // else
            // {
            //     _factor =  Mathf.InverseLerp(0f, Mathf.Abs(_stats.maxSpeed), Mathf.Abs(_frameData.pastVelocity.x));
            // }
            // // _factor = Mathf.InverseLerp(0f, Mathf.Abs(_stats.maxSpeed), Mathf.Abs(_frameData.pastVelocity.x));
            // // _factor *= Mathf.Pow(_timeTillFullAcceleration, -1f);
            // _stats.acceleration *= EasingLib.DerEaseInQuart(_factor, _timeTillFullAcceleration);
            //
            // _pastPastVelocity = _frameData.pastVelocity.x;
            //
            // switch (_playerInput.move.x)
            // {
            //     case > 0:
            //         _factor = Mathf.Lerp(0f, _stats.maxSpeed * Mathf.Sign(_frameData.pastVelocity.x), )
            //         break;
            //     case < 0:
            //         break;
            // }
            //
            // switch (_playerInput.move.x)
            // {
            //     case 0:
            //         _factorPos = Mathf.MoveTowards(_factorPos, 0f, Time.fixedDeltaTime / _timeTillFullAcceleration);
            //         _factorNeg = Mathf.MoveTowards(_factorNeg, 0f, Time.fixedDeltaTime / _timeTillFullAcceleration);
            //         break;
            //     case > 0:
            //         _factorPos = Mathf.MoveTowards(_factorPos, 1f, Time.fixedDeltaTime / _timeTillFullAcceleration);
            //         _factorNeg = Mathf.MoveTowards(_factorNeg, 0f, Time.fixedDeltaTime / _timeTillFullAcceleration);
            //         _stats.acceleration *= EasingLib.DerEaseOutQuad(_factorPos);
            //         break;
            //     default:
            //         _factorPos = Mathf.MoveTowards(_factorPos, 0f, Time.fixedDeltaTime / _timeTillFullAcceleration);
            //         _factorNeg = Mathf.MoveTowards(_factorNeg, 1f, Time.fixedDeltaTime / _timeTillFullAcceleration);
            //         _stats.acceleration *= EasingLib.DerEaseOutQuad(_factorNeg);
            //         break;
            // }
            //
            // return;
            //
            //
            // _time = _playerInput.move.x != 0f
            //     ? _time + Time.fixedDeltaTime
            //     : 0f;
            //
            // var invLerp = Mathf.InverseLerp(0f, _stats.maxSpeed, Mathf.Abs(_frameData.pastVelocity.x));
            // var finalValue = _time / _timeTillFullAcceleration;
            // finalValue = Mathf.Clamp(finalValue, 0f, _timeTillFullAcceleration);
            // _stats.acceleration *= EasingLib.DerEaseOutQuad(finalValue);
        }
    }
}