using System;
using UnityEngine;

namespace Rat
{
    public class CheckpointController : MonoBehaviour
    {
        [SerializeField] private int _price;
        [SerializeField] private GameObject _checkpointPrefab;


        public void Init()
        {
            GameInputManager.Instance.player.interact += Interact;
        }

        private void OnDestroy()
        {
            GameInputManager.Instance.player.interact -= Interact;
        }

        private void Interact()
        {
            if (_price >= GMC_Gameplay.Instance.currentScore)
            {
                return;
            }
            
            
            var checkpoint = Instantiate(_checkpointPrefab, transform.position + new Vector3(0,1,0), Quaternion.identity);
            
            GMC_Gameplay.Instance.SetCurrentScore(GMC_Gameplay.Instance.currentScore - _price);
            GameEvents.OnSaveLocation?.Invoke(checkpoint.transform.position, GMC_Gameplay.Instance.currentScore, gameObject.name);
        }
    }
}