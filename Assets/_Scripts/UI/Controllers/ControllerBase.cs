using UnityEngine;

namespace UI.Controllers
{
    public abstract class ControllerBase : MonoBehaviour
    {
        protected RoomConfig RoomConfig;

        public virtual void OnCreation(RoomConfig roomConfig)
        {
            RoomConfig = roomConfig;
        }
        
        public abstract void OnShow();
        
        public abstract void OnHide();
    }
}