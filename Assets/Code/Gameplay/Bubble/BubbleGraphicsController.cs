using UnityEngine;

namespace Rat
{
    public class BubbleGraphicsController : MonoBehaviour
    {
        private Animator m_animator;
        [SerializeField]
        private Animator m_maskAnimator;
        private static readonly int State = Animator.StringToHash("State");

        public void Init()
        {
            m_animator = GetComponent<Animator>();
        }

        public void SwitchAnimation(AnimationState state)
        {
            switch (state)
            {
                case AnimationState.Idle:
                    m_animator.SetInteger(State, 0);
                    m_maskAnimator?.SetInteger(State, 0);
                    break;
                case AnimationState.Bounce:
                    m_animator.SetInteger(State, 1);
                    m_maskAnimator?.SetInteger(State, 1);
                    break;
                case AnimationState.Expose:
                    m_animator.SetInteger(State, 2);
                    m_maskAnimator?.SetInteger(State, 2);
                    break;
            }
        }
        
        public enum AnimationState
        {
            Idle,
            Bounce,
            Expose
        }
    }
}