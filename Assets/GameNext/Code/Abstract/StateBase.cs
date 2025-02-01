using System;
using UnityEngine;

namespace GameNext
{
    public abstract class StateBase<T_ContextType> : MonoBehaviour, IState where T_ContextType : MonoBehaviour
    {
        public event Action<Type> OnTransitionRequired;

        protected T_ContextType _core;
        
        public virtual void Init(MonoBehaviour core)
        {
            _core = (T_ContextType)core;
        }

        public virtual void Enter()
        {
            gameObject.SetActive(true);
            OnEnter();
        }

        public virtual void Exit()
        {
            OnExit();
            gameObject.SetActive(false);
        }

        protected virtual void OnEnter() {}

        protected virtual void OnExit() {}

        public void RequestTransition<T>() where T : IState
        {
            // Debug.Log(typeof(T).Name);
            OnTransitionRequired?.Invoke(typeof(T));
        }

        private void OnDestroy()
        {
            // OnExit();
        }
    }
}
