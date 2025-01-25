using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Rat
{
    public class GameManager : PersistentSingleton<GameManager>
    {
        [SerializeField] private Player _playerPrefab;
        public Player playerPrefab => _playerPrefab;
        [SerializeField] private int initialLevelIndex = 0;
        [SerializeField] private GameLevelsSO _gameLevelsSO;
        public GameLevelsSO gameLevelsSO => _gameLevelsSO;
        
        public static GameObject currentLevelPrefab { get; private set; }

        public void setCurrentLevel(int levelIndex)
        {
            currentLevelPrefab = _gameLevelsSO.levels[levelIndex].level;
        }
        
        public void SetGameTimeScale(float scale)
        {
            Time.timeScale = scale;
        }

        protected override void Awake()
        {
            base.Awake();
#if UNITY_EDITOR
            setCurrentLevel(initialLevelIndex);
#endif
        }

        public void QuitGame()
        {
            Application.Quit();
#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
#endif
        }
        
        public void LoadScene(string sceneName)
        {
            StartCoroutine(LoadSceneAsync(sceneName));
        }
        
        private IEnumerator LoadSceneAsync(string sceneName)
        {
            yield return SceneManager.LoadSceneAsync(GC.Scenes.EMPTY);
            System.GC.Collect();
            Resources.UnloadUnusedAssets();
            
            yield return SceneManager.LoadSceneAsync(sceneName);

            yield return new WaitForSecondsRealtime(1f);
            
            
            switch (GetType())
            {
                
            }
            
            // SceneSetup();
        }
        
        public void SceneSetup()
        {
            // var gameModeController = GameModeControllerBase.Instance;
            // m_currentCamera = gameModeController.m_camera;
            // gameModeController.Initialize();
        }
        
    }
}