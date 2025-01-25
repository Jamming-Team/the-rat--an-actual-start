using System;
using UnityEngine;

public static class GC
{
    
    public enum Camera
    {
        CameraFollow,
        CameraOverlook,
    }
    
    public static class States
    {
        public static class Game
        {
            public enum MainMenu
            {
                Main,
                LevelSelection,
                HowToPlay,
                Settings,
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
    
    public static class Views
    {
        public static class MainMenu
        {
            public const string LEVEL_SELECTION_BUTTON = "LevelSelectionButton";
            public const string HOW_TO_PLAY_BUTTON = "HowToPlayButton";
            public const string SETTINGS_BUTTON = "SettingsButton";
            
            public const string PLAY_BUTTON = "PlayButton";
        }
    }
    
}
