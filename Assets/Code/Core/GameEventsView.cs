using System;

namespace Rat
{
    public static class GameEventsView
    {
        public static class MainMenu
        {
            public static Action OnPressLevelSelection;
            public static Action OnPressHowToPlay;
            public static Action OnPressSettings;

            public static Action<int> OnPressLevelButton;
            
            public static Action OnPressBackButton;

        }
        
        public static class Gameplay
        {
            public static Action OnPressContinue;

        }
        
        
    }
}