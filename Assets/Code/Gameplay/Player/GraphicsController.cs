using UnityEngine;

namespace Rat
{
    public class GraphicsController : MonoBehaviour
    {
        private static readonly int State = Animator.StringToHash("State");
        private Rigidbody2D rigidbody;
        private Animator m_animator;

        public void Init(Rigidbody2D rigidbody)
        {
            m_animator = GetComponent<Animator>();
            this.rigidbody = rigidbody;
        }

        public void Update()
        {
            if (rigidbody.linearVelocityX != 0)
            {
                gameObject.transform.localRotation = Quaternion.Euler(0, rigidbody.linearVelocityX > 0f ? 0 : 180, 0);
            }
            if (rigidbody.linearVelocityY > 0f)         // Jump
            {
                m_animator.SetInteger(State, 2);
            }
            else if (rigidbody.linearVelocityY < 0f)    // Fall
            {
                m_animator.SetInteger(State, 3);
            }
            else                                        // Run or idle
            {
                if (rigidbody.linearVelocityX != 0f)        //Run
                {
                    m_animator.SetInteger(State, 1);
                }
                else                                    // idle
                {
                    m_animator.SetInteger(State, 0);
                }
            }
        }
    }
}
