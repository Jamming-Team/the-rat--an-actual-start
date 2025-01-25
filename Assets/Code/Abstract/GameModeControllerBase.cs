using UnityEngine;

namespace Rat
{
    public abstract class GameModeControllerBase<T_StateType, T_ContextType> : PersistentSingleton<GameModeControllerBase<T_StateType, T_ContextType>>
    {
        [SerializeField] protected GameObject _statesRoot;
        
        protected StateMachine<T_StateType, T_ContextType> _stateMachine;

        public int currentScore { get; } = 0;
        
        protected override void Awake()
        {
            base.Awake();
#if UNITY_EDITOR
            if (GameManager.Instance == null)
            {
                // typeof(self);
                var assets = UnityEditor.AssetDatabase.FindAssets("GameManager");
                foreach (var guid in assets)
                {
                    var path = UnityEditor.AssetDatabase.GUIDToAssetPath(guid);
                    var prefab = UnityEditor.AssetDatabase.LoadAssetAtPath<GameObject>(path);
                    if (prefab)
                    {
                        Instantiate(prefab);
                        break;
                    }
                }
            }
#endif
            Initialize();
        }
        
        public virtual void Initialize()
        {
            _stateMachine = new StateMachine<T_StateType, T_ContextType>();
            // _stateMachine.Init((T_ContextType)this, _statesRoot);
        }
        
    }
}