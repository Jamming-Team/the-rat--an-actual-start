using System;
using UnityEngine;

namespace MeatAndSoap
{
    public class Player : MonoBehaviour
    {
        [SerializeField]
        private MovementController _movementController;

        private Rigidbody2D _rigidbody2D;
        
        private void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            
            _movementController.Init(_rigidbody2D);
        }
    }
}