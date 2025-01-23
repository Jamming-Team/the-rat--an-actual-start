using System;
using UnityEngine;
using UnityEngine.Serialization;

public abstract class StateBase<T_StateType, T_ContextType> : MonoBehaviour
{
    public event Action<T_StateType> OnTransitionRequired;

    [HideInInspector]
    public T_StateType stateName { get; protected set; }

    protected T_ContextType _context;

    public virtual void Init(T_ContextType context)
    {
        _context = context;
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

    protected abstract void OnEnter();

    protected abstract void OnExit();

    public void RequestTransition(T_StateType nextState)
    {
        // Debug.Log(typeof(T).Name);
        OnTransitionRequired?.Invoke(nextState);
    }

    private void OnDestroy()
    {
        // OnExit();
    }
}