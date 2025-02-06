using System.Collections.Generic;

namespace MeatAndSoap
{
    public interface IPC_States
    {
        public void HandleTransition();
        
        public void HandleModules();
        
        public void HandleInnerForce();
    }
}