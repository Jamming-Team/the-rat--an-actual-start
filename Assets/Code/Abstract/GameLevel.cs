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
            _player = Instantiate(GameManager.Instance.playerPrefab, _playerSpawnPoint.position, Quaternion.identity);
            _player.Init();
        }
    }
}