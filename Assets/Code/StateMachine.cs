using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;


public class StateMachine<T_StateType, T_ContextType>
    {
        [FormerlySerializedAs("m_states")] [SerializeField]
        private  List<StateBase<T_StateType, T_ContextType>> _statesList = new();
        private StateBase<T_StateType, T_ContextType> _currentState;
        public StateBase<T_StateType, T_ContextType> currentState => _currentState;
        
        // protected StateMachine()
        // {
        //     GetComponentsInChildren(m_states);
        //     m_states.ForEach(x =>
        //     {
        //         x.Init(this);
        //         x.OnTransitionRequired += ChangeState;
        //     });
        // }

        public void Init(T_ContextType context, GameObject statesRoot)
        {
            statesRoot.GetComponentsInChildren(_statesList);
            _statesList.ForEach(x =>
            {
                x.Init(context);
                x.OnTransitionRequired += ChangeState;
                x.gameObject.SetActive(false);
                // Debug.Log(x.GetType());
            });
            // m_currentState = m_states[0];
            ChangeState(_statesList[0].stateName);
        }

        public void DeInit()
        {
            _statesList.ForEach(x =>
            {
                x.OnTransitionRequired -= ChangeState;
            });
        }

        public void ChangeState(T_StateType newState)
        {
            var nextState = _statesList.Find(x =>
            {
                return Equals(x.stateName, newState);
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
        
        public void OnDestroy()
        {
            DeInit();
        }
    }
