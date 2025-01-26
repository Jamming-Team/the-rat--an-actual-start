using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Rat
{
    public class PersistentLevelData
    {
        public bool shouldPersist = false;
        public string levelName = "";
        
        public List<string> coinsList = new List<string>();
        public List<string> bubblesList = new List<string>();
        public int currentScore = 0;
        public Vector3 checkPointPosition;
        public string checkPointName = "";
        
        public List<string> coinsListTemp = new List<string>();
        public List<string> bubblesListTemp = new List<string>(); 
    }
    
    public class GameManager : PersistentSingleton<GameManager>
    {
        [SerializeField] private Player _playerPrefab;
        public Player playerPrefab => _playerPrefab;
        [SerializeField] private int initialLevelIndex = 0;
        [SerializeField] private GameLevelsSO _gameLevelsSO;
        public GameLevelsSO gameLevelsSO => _gameLevelsSO;
        public GameLevelData currentLevelData => _gameLevelsSO.levels[_currentLevelIndex];
        private int _currentLevelIndex;

        public PersistentLevelData persistentLevelData = new PersistentLevelData();
        
        public static GameObject currentLevelPrefab { get; private set; }

        public void setCurrentLevel(int levelIndex)
        {
            currentLevelPrefab = _gameLevelsSO.levels[levelIndex].level;
            _currentLevelIndex = levelIndex;
        }
        
        public void SetGameTimeScale(float scale)
        {
            Time.timeScale = scale;
        }

        protected override void Awake()
        {
            Debug.Log("Game Manager Awake");
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
        
        private bool _isLoading = false;
        
        public void LoadScene(string sceneName)
        {
            if (_isLoading)
                return;
            
            // StopCoroutine(LoadSceneAsync(sceneName));
            Debug.Log($"LoadSceneCount: {SceneManager.loadedSceneCount}");
            _isLoading = true; 
            // SceneManager.LoadScene(sceneName);
            
            StartCoroutine(LoadSceneAsync(sceneName));
        }
        
        private IEnumerator LoadSceneAsync(string sceneName)
        {
            if (sceneName == GC.Scenes.MAIN_MENU)
            {
                persistentLevelData = new();
            }
            // System.GC.Collect();
            // Resources.UnloadUnusedAssets();
            yield return SceneManager.LoadSceneAsync(GC.Scenes.EMPTY);
            System.GC.Collect();
            Resources.UnloadUnusedAssets();
            
            yield return SceneManager.LoadSceneAsync(sceneName);

            _isLoading = false;
            
            // yield return new WaitForSecondsRealtime(1f);
            

            
            // SceneSetup();
        }
        
        public void SceneSetup()
        {
            // var gameModeController = GameModeControllerBase.Instance;
            // m_currentCamera = gameModeController.m_camera;
            // gameModeController.Initialize();
        }

        public void SaveScore(int score)
        {
            _gameLevelsSO.levels[_currentLevelIndex].playerScore = score;
        }
        
    }
}