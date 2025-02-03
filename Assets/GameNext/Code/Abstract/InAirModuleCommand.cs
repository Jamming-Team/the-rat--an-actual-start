using UnityEngine;

namespace GameNext
{
    public abstract class InAirModuleCommand : MonoBehaviour, ICommand
    {
        protected MCStatsData.InAir _stats;
        protected MovementController.FrameData _frameData;
        protected MovementController.FrameInput _playerInput;
        
        public void Init(MCStatsData.InAir stats, MovementController.FrameData frameData, MovementController.FrameInput playerInput)
        {
            _stats = stats;
            _frameData = frameData;
            _playerInput = playerInput;
        }
        
        public abstract void Execute();
    }
}