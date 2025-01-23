using UnityEngine;
using UnityEngine.InputSystem;
using System;


public class GameInputManager : MonoBehaviour
{

    public class Player
    {
        public Action<bool> grab;
        public Action<Vector2> move;
        public Action sprint;
        public Action interact;
        public Action<Vector2> look;
    }
    
    public class Vehicle
    {
        public Action<Vector2> move;
        public Action nitro;
        public Action interact;
        public Action<Vector2> look;
    }

    [SerializeField] private GameObject _statesRoot;
    
    [HideInInspector]
    public Player player = new Player();
    [HideInInspector]
    public Vehicle vehicle = new Vehicle();
    
    private StateMachine<GC.States.InputMaps, GameInputManager> _stateMachine;


    public void Init()
    {
        _stateMachine = new StateMachine<GC.States.InputMaps, GameInputManager>();
        _stateMachine.Init(this, _statesRoot);
    }

    public void DeInit()
    {
        _stateMachine.DeInit();
    }

    public void ChangeState(GC.States.InputMaps newState)
    {
        _stateMachine.ChangeState(newState);
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        // _playerInput = GetComponent<PlayerInput>();
        
        // _inputSystem = new InputSystem_Actions();
        
        Activate();

    }

    public void Activate()
    {
        // _inputSystem.Enable();
        // _inputSystem.Player.Disable();
        // _playerInput.actions.FindActionMap("dfgfgd").Enable();
        // _inputSystem.Player.Attack.performed += AttackOnperformed;
        // _inputSystem.Actual.A.performed += AttackOnperformed;

        // void AttackOnperformed(InputAction.CallbackContext obj)
        // {
        //     Debug.Log("dfgfghgfdhghf");
        // }
        
        // _inputSystem.Player.Move.
        
        
        
    }



}
