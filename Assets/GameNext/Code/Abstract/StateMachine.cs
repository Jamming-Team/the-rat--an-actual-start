using System.Collections.Generic;
using UnityEngine;

namespace GameNext
{
    public class StateMachine : MonoBehaviour
    {
        private  List<IState> _states = new();
        private IState _currentState;
        public IState currentState => _currentState;
        
        
        
        // protected StateMachine()
        // {
        //     GetComponentsInChildren(m_states);
        //     m_states.ForEach(x =>
        //     {
        //         x.Init(this);
        //         x.OnTransitionRequired += ChangeState;
        //     });
        // }

        public void Init(MonoBehaviour core)
        {
            GetComponentsInChildren(_states);
            _states.ForEach(x =>
            {
                x.Init(core);
                x.OnTransitionRequired += ChangeState;
                // Debug.Log(x.GetType());
            });
            // m_currentState = m_states[0];
            ChangeState(_states[0].GetType());
        }
        
        public void OnDestroy()
        {
            _states.ForEach(x =>
            {
                x.OnTransitionRequired -= ChangeState;
            });
        }

        public void ChangeState(System.Type nextStateType)
        {
            var nextState = _states.Find(x =>
            {
                return x.GetType() == nextStateType;
            });
            
            // Debug.Log(nextState);
            
            // Debug.Log(!Equals(m_currentState, nextState));
            
            
            if (nextState != null && !Equals(_currentState, nextState))
            {
                if (_currentState != null)
                {
                    _currentState.Exit();
                }
                nextState.Enter();
                _currentState = nextState;
                // Debug.Log(m_currentState);
            }
        }
    }
}
