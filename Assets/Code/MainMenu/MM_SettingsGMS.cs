namespace Rat
{
    public class MM_SettingsGMS : GameModeStateBase<GC.States.Game.MainMenu, GMC_MainMenu>
    {
        public override void Init(GMC_MainMenu context)
        {
            base.Init(context);
            stateName = GC.States.Game.MainMenu.Settings;
        }
        
        protected override void OnEnter()
        {
            base.OnEnter();
            GameEventsView.MainMenu.OnPressBackButton += OnPressBack;
        }
        
        protected override void OnExit()
        {
            base.OnExit();
            GameEventsView.MainMenu.OnPressBackButton -= OnPressBack;
        }
        
        private void OnPressBack()
        {
            RequestTransition(GC.States.Game.MainMenu.Main);
        }
    }
}