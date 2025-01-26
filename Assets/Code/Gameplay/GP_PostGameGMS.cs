namespace Rat
{
    public class GP_PostGameGMS : GameModeStateBase<GC.States.Game.Gameplay, GMC_Gameplay>
    {
        public override void Init(GMC_Gameplay context)
        {
            base.Init(context);
            stateName = GC.States.Game.Gameplay.PostGame;
        }
        
        protected override void OnEnter()
        {
            base.OnEnter();
            GameEventsView.Gameplay.OnPressRestart += OnPressRestart;
            GameEventsView.Gameplay.OnPressToMainMenu += OnPressToMainMenu;
        }

        protected override void OnExit()
        {
            base.OnExit();
            GameEventsView.Gameplay.OnPressRestart -= OnPressRestart;
            GameEventsView.Gameplay.OnPressToMainMenu -= OnPressToMainMenu;
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