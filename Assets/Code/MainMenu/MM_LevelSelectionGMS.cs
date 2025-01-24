using UnityEngine;

namespace Rat
{
    public class MM_LevelSelectionGMS : GameModeStateBase<GC.States.Game.MainMenu, GMC_MainMenu>
    {
        
        public override void Init(GMC_MainMenu context)
        {
            base.Init(context);
            stateName = GC.States.Game.MainMenu.LevelSelection;
        }
        
        protected override void OnEnter()
        {
            base.OnEnter();
            GameEventsView.MainMenu.OnPressPlay += OnPressPlay;
        }
        
        protected override void OnExit()
        {
            base.OnExit();
            GameEventsView.MainMenu.OnPressPlay -= OnPressPlay;
        }

        private void OnPressPlay()
        {
            Debug.Log("OnPressPlay");
        }
    }
}