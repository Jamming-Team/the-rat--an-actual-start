using System;
using System.Collections.Generic;
using System.Reflection;
using GameNext.GameNext.Code.SM.Gameplay.PC;
using UnityEngine;

namespace GameNext.GameNext.Code
{
    public class MCDataFillerVisitor : MonoBehaviour, IVisitor
    {
        [SerializeField]
        private MCStatsSO _statsSO;

        public void FillAllTheData()
        {
            List<IVisitable> visitablesList = new();
            
            GetComponentsInChildren(visitablesList);
            
            visitablesList.ForEach(v => v.Accept(this));
        }
        
        public void Visit(object o)
        {
            MethodInfo visitMethod = GetType().GetMethod("Visit", new Type[] { o.GetType() });
            if (visitMethod != null && visitMethod != GetType().GetMethod("Visit", new Type[] { typeof(object) }))
            {
                visitMethod.Invoke(this, new object[] { o });
            }
            else
            {
                DefaultVisit(o);
            }
        }
        
        private void DefaultVisit(object o)
        {
            // noop (== `no op` == `no operation`)
            Debug.Log("MCDataFillerVisitor.DefaultVisit");
        }

        // States
        
        public void Visit(Grounded grounded)
        {
            grounded.FillData(_statsSO.grounded);
        }
        
        public void Visit(InAir inAir)
        {
            inAir.FillData(_statsSO.inAir);
        }
        
        public void Visit(NonParabolicJump nonParabolicJump)
        {
            nonParabolicJump.FillData(_statsSO.nonParabolicJump);
        }
        
        public void Visit(VariableJumpHeight variableJumpHeight)
        {
            variableJumpHeight.FillData(_statsSO.variableJumpHeight);
        }
        
    }
}