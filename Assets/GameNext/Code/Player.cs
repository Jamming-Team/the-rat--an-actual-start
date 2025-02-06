using System;
using UnityEngine;

namespace MeatAndSoap
{
    public class Player : MonoBehaviour
    {
        [SerializeField]
        private InputReader _inputReader;
        [SerializeField]
        private MovementController _movementController;

        private Rigidbody2D _rigidbody2D;
        
        private void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            
            _movementController.Init(_rigidbody2D);
            
            _inputReader.EnablePlayerActions();
        }

        private void Update()
        {
            _movementController.SupplyInput(_inputReader.move, _inputReader.jumpPerformed, _inputReader.jumpIsBeingPressed);
            // Debug.Log($"Move: {_inputReader.move}, jumpP: {_inputReader.jumpPerformed}, jumpInP: {_inputReader.jumpIsBeingPressed}");
        }
    }
}