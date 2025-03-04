using UnityEngine;

namespace UI.Controllers
{
    public abstract class ControllerBase : MonoBehaviour
    {
        protected UIController Context;

        public virtual void OnCreation(UIController context)
        {
            Context = context;
        }
        
        public abstract void OnShow();
        
        public abstract void OnHide();
    }
}