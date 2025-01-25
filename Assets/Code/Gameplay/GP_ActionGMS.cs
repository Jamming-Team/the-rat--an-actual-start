namespace Rat
{
    public class GP_ActionGMS : GameModeStateBase<GC.States.Game.Gameplay, GMC_Gameplay>
    {
        public override void Init(GMC_Gameplay context)
        {
            base.Init(context);
            stateName = GC.States.Game.Gameplay.Action;
        }
    }
}