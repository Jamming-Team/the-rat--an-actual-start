namespace Rat
{
    public class MM_SettingsGMS : GameModeStateBase<GC.States.Game.MainMenu, GMC_MainMenu>
    {
        public override void Init(GMC_MainMenu context)
        {
            base.Init(context);
            stateName = GC.States.Game.MainMenu.Settings;
        }
    }
}