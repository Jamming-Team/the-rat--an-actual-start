using System.Collections.Generic;
using UnityEngine;

namespace MeatAndSoap
{
    public interface IPC_States
    {
        public object statsRef { get; }
        public MovementController coreRef { get; } 
            
        public void HandleTransition();
        
        public void HandleModules();
        
        public void HandleInnerForce();
    }
}