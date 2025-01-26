// using System;
// using TMPro;
// using UnityEngine;
//
// namespace Rat
// {
//     public class Checkpoint : MonoBehaviour
//     {
//         [SerializeField]
//         private LayerMask _playerLayerMask;
//         [SerializeField] private int _price;
//
//         [SerializeField] private GameObject _focusGO;
//         [SerializeField] private TMP_Text _focusedTextScore;
//         
//         private bool isActive = false;
//         private bool isFocused = false;
//         
//
//         private void Awake()
//         {
//             var data = GameManager.Instance.persistentLevelData;
//             if (data.shouldPersist && data.levelName == GameManager.Instance.currentLevelData.name)
//             {
//                 if (data.checkPointName == gameObject.name)
//                     isActive = true;
//             }
//             
//             _focusedTextScore.text = _price.ToString();
//             _focusGO.SetActive(false);
//             GameInputManager.Instance.player.interact += Interact;
//         }
//
//         private void Interact()
//         {
//             Debug.Log("Interact2");
//             if (!(!isActive && isFocused && _price <= GMC_Gameplay.Instance.currentScore))
//             {
//                 return;
//             }
//             Debug.Log("Interact3");
//
//             GMC_Gameplay.Instance.SetCurrentScore(GMC_Gameplay.Instance.currentScore - _price);
//             GameEvents.OnSaveLocation?.Invoke(transform.position, GMC_Gameplay.Instance.currentScore, gameObject.name);
//             isActive = true;
//             isFocused = false;
//             _focusGO.SetActive(false);
//         }
//
//         private void OnTriggerEnter2D(Collider2D other)
//         {
//             Debug.Log("Checkpoint 1");
//             if ((_playerLayerMask.value & (1 << other.gameObject.layer)) != 0 && !isActive)
//             {
//                 Debug.Log("Checkpoint triggered");
//                 isFocused = true;
//                 _focusGO.SetActive(true);
//             }
//         }
//         
//         private void OnTriggerExit2D(Collider2D other)
//         {
//             if ((_playerLayerMask.value & (1 << other.gameObject.layer)) != 0 && !isActive)
//             {
//                 isFocused = false;
//                 _focusGO.SetActive(false);
//             }
//         }
//     }
// }