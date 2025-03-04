using System.Threading.Tasks;

#if UNITY_WEBGL && !UNITY_EDITOR 

#else
using Firebase.Database;
using UnityEngine;
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
            Debug.Log(data);
        }
#else
        
        protected override void GetReference()
        {
            Reference = FirebaseDatabase.DefaultInstance.GetReference(Room);
        }

        protected override void HandleChildChanged(object sender, ChildChangedEventArgs e)
        {
            Debug.Log("Child changed/added: " + e.Snapshot.Key);
        }

#endif
        
        
    }
}