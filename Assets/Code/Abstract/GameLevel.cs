using System;
using System.Collections.Generic;
using UnityEngine;

namespace Rat
{
    public class GameLevel : MonoBehaviour
    {
        [SerializeField] protected Transform _playerSpawnPoint;
        private Player _player;
        public Player player => _player;
        
        public void InitLevel()
        {
            HandlePersistentCase(GameManager.Instance.persistentLevelData);
            
            _player = Instantiate(GameManager.Instance.playerPrefab, _playerSpawnPoint.position, Quaternion.identity);
            _player.Init();
            
        }

        public void HandlePersistentCase(PersistentLevelData data)
        {
            data.coinsListTemp.Clear();
            data.bubblesListTemp.Clear();
            
            if (!data.shouldPersist || data.levelName != GameManager.Instance.currentLevelData.name)
            {
                GameManager.Instance.persistentLevelData = new();
                return;
            }
            
            GMC_Gameplay.Instance.SetCurrentScore(data.currentScore);
            _playerSpawnPoint.position = data.checkPointPosition;
        }
        
        
        
    }
}