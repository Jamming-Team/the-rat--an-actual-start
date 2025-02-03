using UnityEngine;

namespace GameNext
{
    public abstract class GroundedModuleCommands : MonoBehaviour, ICommand
    {
        protected MCStatsData.Grounded _stats;
        protected MovementController.FrameData _frameData;
        protected MovementController.FrameInput _playerInput;
        
        public void Init(MCStatsData.Grounded stats, MovementController.FrameData frameData, MovementController.FrameInput playerInput)
        {
            _stats = stats;
            _frameData = frameData;
            _playerInput = playerInput;
        }
        
        public abstract void Execute();
    }
}