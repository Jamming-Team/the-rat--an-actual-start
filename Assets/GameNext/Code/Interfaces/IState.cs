using System;
using UnityEngine;

namespace GameNext
{
    public interface IState
    {
        public event Action<Type> OnTransitionRequired;
        
        public void Init(MonoBehaviour core);

        public void Enter();
        
        public void Exit();
        
        public void RequestTransition<T>() where T : IState;
    }
}