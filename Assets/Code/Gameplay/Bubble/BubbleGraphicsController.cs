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
                    m_animator?.SetInteger(State, 0);
                    m_maskAnimator?.SetInteger(State, 0);
                    break;
                case AnimationState.Bounce:
                    m_animator?.SetTrigger("Bounce");
                    m_maskAnimator?.SetTrigger("Bounce");
                    break;
                case AnimationState.Expose:
                    m_animator?.SetTrigger("Expose");
                    m_maskAnimator?.SetTrigger("Expose");
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