using System;
using UnityEngine;

public static class GC
{

    public static class States
    {
        public static class Game
        {
            public enum MainMenu
            {
                Main,
                Settings,
                LevelSelection,
            }
            
            public enum Gameplay
            {
                Action,
                Pause,
                PostGame,
            }
        }

        public enum InputMaps
        {
            None,
            Player,
            Vehicle
        }
        
        public static class Input
        {
            
        }
    }

    public static class Scenes
    {
        public const string EMPTY = "Empty";
        public const string MAIN_MENU = "MainMenu";
        public const string GAMEPLAY = "Gameplay";
    }
    
}
