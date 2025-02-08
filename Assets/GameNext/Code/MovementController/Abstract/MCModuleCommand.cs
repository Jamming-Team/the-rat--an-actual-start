using UnityEngine;

namespace MeatAndSoap
{
    public abstract class MCModuleCommand<T> : MonoBehaviour, ICommandMCModule
    {
        protected T _stats;
        protected MovementController _mc;
        
        
        public void Init(IPC_States state)
        {
            _stats = (T)state.statsRef;
            _mc = state.coreRef;
        }
        
        public virtual void Execute() {}
    }
}