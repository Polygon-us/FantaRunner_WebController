#if UNITY_WEBGL && !UNITY_EDITOR 

#else
using Firebase.Database;
#endif
using FirebaseCore.DTOs;
using UnityEngine;

namespace FirebaseCore.Listeners
{
    public class UserListener : FirebaseListener<UserDataDto>
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
            Reference = FirebaseDatabase.DefaultInstance.GetReference($"{Room}/{UserCollection}");
        }

        protected override void HandleChildChanged(object sender, ChildChangedEventArgs e)
        {
            Debug.Log("Child changed/added: " + e.Snapshot.Key + " " + e.Snapshot.Value);
        }

#endif
        
    }
}