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
                MainView,
                SettingsView,
            }
            
            public enum Gameplay
            {
                Action,
                Pause,
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
