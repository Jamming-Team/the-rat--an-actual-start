using System;
using TriInspector;
using UnityEngine;
using UnityEngine.Serialization;

namespace MeatAndSoap
{
    public class Player : MonoBehaviour
    {
        [SerializeField]
        private InputReader _inputReader;

        // [ShowInInspector]
        [SerializeField]
        private MovementController _movementComponent;
        public MovementController movementController => _movementComponent; 

        private Rigidbody2D _rigidbody2D;
        
        private void Awake()
        {
            Debug.Log(2);//

            _rigidbody2D = GetComponent<Rigidbody2D>();
            
            movementController.Init(_rigidbody2D);
            
            _inputReader.EnablePlayerActions();
        }

        private void Update()
        {
            movementController.SupplyInput(_inputReader.move, _inputReader.jumpPerformed, _inputReader.jumpIsBeingPressed);
            // Debug.Log($"Move: {_inputReader.move}, jumpP: {_inputReader.jumpPerformed}, jumpInP: {_inputReader.jumpIsBeingPressed}");
        }
    }
}