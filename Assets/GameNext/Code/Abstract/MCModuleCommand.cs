using UnityEngine;

namespace GameNext
{
    public abstract class MCModuleCommand<T> : MonoBehaviour, ICommand
    {
        protected T _stats;
        protected MovementController _mc;
        
        public void Init(T stats, MovementController mc)
        {
            _stats = stats;
            _mc = mc;
        }
        
        public virtual void Execute() {}
    }
}