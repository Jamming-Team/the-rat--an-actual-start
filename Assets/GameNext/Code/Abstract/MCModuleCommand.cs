using UnityEngine;

namespace GameNext
{
    public abstract class MCModuleCommand<T> : MonoBehaviour, ICommand
    {
        protected T _stats;
        protected MovementController.FrameData _frameData;
        protected MovementController.FrameInput _playerInput;
        
        public void Init(T stats, MovementController.FrameData frameData, MovementController.FrameInput playerInput)
        {
            _stats = stats;
            _frameData = frameData;
            _playerInput = playerInput;
        }
        
        public virtual void Execute() {}
    }
}