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
        
        private void OnGrab(InputValue value)
        {
        
        }

        public void FireAction<T>(T value)
        {
            
        }
    }
    
    public class Vehicle
    {
        public Action<Vector2> move;
        public Action nitro;
        public Action interact;
        public Action<Vector2> look;
    }
    
    // private PlayerInput _playerInput;
    private InputSystem_Actions _inputSystem;
    
    // private PlayerControls _playerControls;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        // _playerInput = GetComponent<PlayerInput>();
        
        _inputSystem = new InputSystem_Actions();
        
        Activate();

    }

    public void Activate()
    {
        // _inputSystem.Enable();
        // _inputSystem.Player.Disable();
        // _playerInput.actions.FindActionMap("dfgfgd").Enable();
        _inputSystem.Player.Attack.performed += AttackOnperformed;
        _inputSystem.Actual.A.performed += AttackOnperformed;

        void AttackOnperformed(InputAction.CallbackContext obj)
        {
            Debug.Log("dfgfghgfdhghf");
        }
        
        // _inputSystem.Player.Move.
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnAttack(InputValue value)
    {
        Debug.Log(value.isPressed);
        Debug.Log("dfgfghgfdhghf");
    }
    
    private void OnA()
    {

        Debug.Log("fghhgfhgf");
    }


}
