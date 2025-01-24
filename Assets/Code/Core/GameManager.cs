using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Rat
{
    public class GameManager : PersistentSingleton<GameManager>
    {
        
        public void SetGameTimeScale(float scale)
        {
            Time.timeScale = scale;
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