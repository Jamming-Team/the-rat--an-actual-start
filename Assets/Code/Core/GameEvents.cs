using System;
using UnityEngine;

namespace Rat
{
    public static class GameEvents
    {
        public static Action<int> OnCoinCollected;
        public static Action<string> OnCoinCollectedPersist;
        public static Action<string> OnBubbleDestroyedPersist;

        public static Action OnEnteredFinish;
        

        public static Action<Vector3, int, string> OnSaveLocation;

        public static Action<Player> OnDeathEvent;
    }
}