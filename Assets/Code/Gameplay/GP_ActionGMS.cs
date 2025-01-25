namespace Rat
{
    public class GP_ActionGMS : GameModeStateBase<GC.States.Game.Gameplay, GMC_Gameplay>
    {
        public override void Init(GMC_Gameplay context)
        {
            base.Init(context);
            stateName = GC.States.Game.Gameplay.Action;
        }
        
        protected override void OnEnter()
        {
            base.OnEnter();
            GameInputManager.Instance.player.pause += Pause;
            GameEventsView.Gameplay.OnPressPause += Pause;
        }

        protected override void OnExit()
        {
            base.OnExit();
            GameInputManager.Instance.player.pause -= Pause;
            GameEventsView.Gameplay.OnPressPause -= Pause;
        }
        
        private void Pause()
        {
            RequestTransition(GC.States.Game.Gameplay.Pause);
        }
    }
}