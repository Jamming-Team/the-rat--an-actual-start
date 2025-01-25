using UnityEngine;

namespace Rat
{
    public class GameLevel : MonoBehaviour
    {
        [SerializeField] protected Transform _playerSpawnPoint;

        public void InitLevel()
        {
            var player = Instantiate(GameManager.Instance.playerPrefab, _playerSpawnPoint.position, Quaternion.identity);
            player.Init();
        }
    }
}