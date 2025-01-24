using System;
using UnityEngine;

namespace Rat
{
    public class GamePreloader : MonoBehaviour
    {
        private void Start()
        {
            GameManager.Instance.LoadScene(GC.Scenes.MAIN_MENU);
        }
    }
}