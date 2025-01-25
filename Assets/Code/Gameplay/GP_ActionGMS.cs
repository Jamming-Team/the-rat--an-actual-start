using UnityEngine;

namespace Rat
{
    public class GP_ActionGMS : GameModeStateBase<GC.States.Game.Gameplay, GMC_Gameplay>
    {
        private bool _overlookIsPressed = false;
        
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
            
            GameInputManager.Instance.player.overlook += Overlook;
            
            ManageOverlook();
        }

        protected override void OnExit()
        {
            base.OnExit();
            GameInputManager.Instance.player.pause -= Pause;
            GameEventsView.Gameplay.OnPressPause -= Pause;
            
            GameInputManager.Instance.player.overlook -= Overlook;
        }
        
        private void Pause()
        {
            RequestTransition(GC.States.Game.Gameplay.Pause);
        }
        
        private void Overlook(bool obj)
        {
            _overlookIsPressed = obj;
            ManageOverlook();
            Debug.Log("Overlook");
        }

        private void ManageOverlook()
        {
            GMC_Gameplay.Instance.cameraManager.SetCurrentCamera(_overlookIsPressed ? GC.Camera.CameraOverlook : GC.Camera.CameraFollow);
        }
    }
}