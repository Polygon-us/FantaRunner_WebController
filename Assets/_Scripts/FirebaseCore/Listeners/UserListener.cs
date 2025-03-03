using System.Threading.Tasks;

#if UNITY_WEBGL && !UNITY_EDITOR 

#else
using Firebase.Database;
#endif

namespace FirebaseCore.Listeners
{
    public class UserListener : FirebaseListener
    {
        private const string UserCollection = "user";
        
        public UserListener(string room) : base(room)
        {
        }
        
#if UNITY_WEBGL && !UNITY_EDITOR 
        protected override void HandleValueChanged(string data)
        {   
            
        
        }
#else
        protected override Task Connect()
        {
            return base.Connect();
            
            Reference = FirebaseDatabase.DefaultInstance.GetReference(Room);
        }
#endif
        
        
    }
}