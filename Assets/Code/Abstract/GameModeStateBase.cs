using System.Collections.Generic;
using UnityEngine;

namespace Rat
{
    public abstract class GameModeStateBase<T_StateType, T_ContextType> : StateBase<T_StateType, T_ContextType>
    {
        [SerializeField] protected List<GameObject> _views;

        public override void Init(T_ContextType context)
        {
            base.Init(context);
            SetViewsVisibility(false);
        }
        
        protected override void OnEnter()
        {
            SetViewsVisibility(true);
        }

        protected override void OnExit()
        {
            SetViewsVisibility(false);
        }
        
        protected void SetViewsVisibility(bool visibility)
        {
            _views?.ForEach(x =>
            {
                Debug.Log(x.gameObject.name);
                if (x)
                    x.SetActive(visibility);
            });
        }
    }
}