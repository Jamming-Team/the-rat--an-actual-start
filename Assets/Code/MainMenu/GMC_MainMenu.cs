namespace Rat
{
    public class GMC_MainMenu : GameModeControllerBase<GC.States.Game.MainMenu, GMC_MainMenu>
    {
        public override void Initialize()
        {
            base.Initialize();
            // _stateMachine = new StateMachine<T_StateType, T_ContextType>();
            _stateMachine.Init(this, _statesRoot);
        }
    }
}