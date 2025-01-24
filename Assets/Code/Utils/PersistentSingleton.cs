using UnityEngine.Serialization;

namespace Rat
{
    using UnityEngine;
    
    public class PersistentSingleton<T> : MonoBehaviour where T : Component
    {
        [SerializeField] private bool _autoUnparentOnAwake = true;
        [SerializeField] private bool _autoMakeDDOLOnAwake = false;

        protected static T instance;

        public static bool HasInstance => instance != null;
        public static T TryGetInstance() => HasInstance ? instance : null;

        
        
        public static T Instance {
            get {
                if (instance == null)
                {
                    return null; 
                    // Debug.LogError($"Instance of {typeof(T)} not found");
                    // instance = FindAnyObjectByType<T>();
                    // if (instance == null) {
                    //     var go = new GameObject(typeof(T).Name + " Auto-Generated");
                    //     instance = go.AddComponent<T>();
                    // }
                }

                return instance;
            }
        }

        /// <summary>
        /// Make sure to call base.Awake() in override if you need awake.
        /// </summary>
        protected virtual void Awake() {
            InitializeSingleton();
        }

        protected virtual void InitializeSingleton() {
            if (!Application.isPlaying) return;

            if (_autoUnparentOnAwake) {
                transform.SetParent(null);
            }

            if (instance == null) {
                instance = this as T;
            }
            else {
                if (instance != this) {
                    Destroy(gameObject);
                }
            }

            if (_autoMakeDDOLOnAwake)
            {
                DontDestroyOnLoad(gameObject);
            }
        }
    }
}