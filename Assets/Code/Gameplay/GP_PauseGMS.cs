namespace Rat
{
    public class GP_PauseGMS : GameModeStateBase<GC.States.Game.Gameplay, GMC_Gameplay>
    {
        public override void Init(GMC_Gameplay context)
        {
            base.Init(context);
            stateName = GC.States.Game.Gameplay.Pause;
        }
        
        protected override void OnEnter()
        {
            base.OnEnter();
            GameInputManager.Instance.player.pause += Continue;
            GameEventsView.Gameplay.OnPressContinue += Continue;
            GameEventsView.Gameplay.OnPressRestart += OnPressRestart;
            GameEventsView.Gameplay.OnPressToMainMenu += OnPressToMainMenu;
        }

        protected override void OnExit()
        {
            base.OnExit();
            GameInputManager.Instance.player.pause -= Continue;
            GameEventsView.Gameplay.OnPressContinue -= Continue;
            GameEventsView.Gameplay.OnPressRestart -= OnPressRestart;
            GameEventsView.Gameplay.OnPressToMainMenu -= OnPressToMainMenu;
        }
        
        private void Continue()
        {
            RequestTransition(GC.States.Game.Gameplay.Action);
        }
        
        private void OnPressRestart()
        {
            GameManager.Instance.LoadScene(GC.Scenes.GAMEPLAY);
        }
        
        private void OnPressToMainMenu()
        {
            GameManager.Instance.LoadScene(GC.Scenes.MAIN_MENU);
        }
    }
}