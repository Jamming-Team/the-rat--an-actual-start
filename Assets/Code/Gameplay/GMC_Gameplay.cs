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
            cameraManager.Setup(_currentGameLevel.player.transform, GameManager.Instance.currentLevelData.overlookCamera);
            
            GameEvents.OnCoinCollected += OnCoinCollected;
            GameEvents.OnCoinCollectedPersist += OnCoinCollectedPersist;
            GameEvents.OnSaveLocation += OnSaveLocation;
            GameEvents.OnBubbleDestroyedPersist += OnBubbleDestroyedPersist;

        }

        private void OnBubbleDestroyedPersist(string obj)
        {
            GameManager.Instance.persistentLevelData.bubblesListTemp.Add(obj);
        }

        private void OnSaveLocation(Vector3 arg1, int arg2, string arg3)
        {
            var data = GameManager.Instance.persistentLevelData;
            data.checkPointPosition = arg1;
            data.currentScore = arg2;
            data.checkPointName = arg3;
            data.levelName = GameManager.Instance.currentLevelData.name;

            data.coinsListTemp.ForEach(x =>
            {
                data.coinsList.Add(x);
            });
            data.bubblesListTemp.ForEach(x =>
            {
                data.bubblesList.Add(x);
            });

            
            GameManager.Instance.persistentLevelData.shouldPersist = true;
        }

        private void OnCoinCollectedPersist(string obj)
        {
            GameManager.Instance.persistentLevelData.coinsListTemp.Add(obj);
        }

        private void OnCoinCollected(int obj)
        {
            SetCurrentScore(currentScore + obj);
        }
        
        
        
        public void Test()
        {
            
        }
        
    }
}