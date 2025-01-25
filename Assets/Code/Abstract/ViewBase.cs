using UnityEngine;
using UnityEngine.UIElements;

namespace Rat
{
    public abstract class ViewBase : MonoBehaviour
    {
        // protected VisualElement m_view;
        
        protected virtual void OnEnable()
        {
            // m_view = GetComponent<UIDocument>().rootVisualElement;
        }
    }
}