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
        Debug.Log($"It's me, {this.GetType()}, and I am initialized");
        _context = context;
    }

    public virtual void Enter()
    {
        Debug.Log($"It's me, {this.GetType()}, and I am being entered");
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