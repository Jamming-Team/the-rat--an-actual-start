using UnityEngine;

namespace Rat
{
    public class GMC_Gameplay : GameModeControllerBase<GC.States.Game.Gameplay, GMC_Gameplay>
    {
        // [SerializeField]
        // private MovementComponent _movementComponent;
        //
        
        
        public GameLevel _currentGameLevel;



        public override void Initialize()
        {
            base.Initialize();
            // _stateMachine = new StateMachine<T_StateType, T_ContextType>();
            _stateMachine.Init(this, _statesRoot);
            // _movementComponent.Spawn();

            _currentGameLevel = Instantiate(GameManager.currentLevelPrefab).GetComponent<GameLevel>();
            _currentGameLevel.InitLevel();
        }

        public void Test()
        {
            
        }
        
    }
}