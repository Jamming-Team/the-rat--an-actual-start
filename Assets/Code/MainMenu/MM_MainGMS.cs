using UnityEngine;

namespace Rat
{
    public class MM_MainGMS : GameModeStateBase<GC.States.Game.MainMenu, GMC_MainMenu>
    {
        public override void Init(GMC_MainMenu context)
        {
            base.Init(context);
            stateName = GC.States.Game.MainMenu.Main;
        }
        
        protected override void OnEnter()
        {
            base.OnEnter();
            // GameEventsView.MainMenu.OnPressHowToPlay += OnPressHowToPlay;
            GameEventsView.MainMenu.OnPressLevelSelection += OnPressLevelSelection;
            GameEventsView.MainMenu.OnPressSettings += OnPressSettings;
        }
        
        protected override void OnExit()
        {
            base.OnExit();
            // GameEventsView.MainMenu.OnPressHowToPlay -= OnPressHowToPlay;
            GameEventsView.MainMenu.OnPressLevelSelection -= OnPressLevelSelection;
            GameEventsView.MainMenu.OnPressSettings -= OnPressSettings;
        }
        
        private void OnPressLevelSelection()
        {
            RequestTransition(GC.States.Game.MainMenu.LevelSelection);
        }

        // private void OnPressHowToPlay()
        // {
        //     RequestTransition(GC.States.Game.MainMenu.HowToPlay);
        // }
        
        private void OnPressSettings()
        {
            Debug.Log("OnPressSettings");
            RequestTransition(GC.States.Game.MainMenu.Settings);
        }
        

    }
}