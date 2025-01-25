using UnityEngine;

namespace Rat
{
    public class GraphicsController : MonoBehaviour
    {
        private Animator m_animator;

        public void Init()
        {
            m_animator = GetComponent<Animator>();
        }
    }
}