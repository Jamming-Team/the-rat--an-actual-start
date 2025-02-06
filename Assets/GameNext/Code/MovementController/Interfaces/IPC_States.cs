using System.Collections.Generic;

namespace GameNext
{
    public interface IPC_States
    {
        public void HandleTransition();
        
        public void HandleModules();
        
        public void HandleInnerForce();
    }
}