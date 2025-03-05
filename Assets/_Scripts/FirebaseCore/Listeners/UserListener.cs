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
        protected override string ChildName { get; set; } = "user";

        public UserListener(string room) : base(room)
        {
        }
        
#if UNITY_WEBGL && !UNITY_EDITOR 
        protected override void HandleValueChanged(string data)
        {   
            Debug.Log(data);
            OnDataReceived?.Invoke(new UserDataDto());
        }
#else

        protected override void HandleChildChanged(object sender, ChildChangedEventArgs e)
        {
            Debug.Log("Child changed/added: " + e.Snapshot.Key + " " + e.Snapshot.Value);
            
            // OnDataReceived?.Invoke(ConvertTo<UserDataDto>(e.Snapshot.Value));
            OnDataReceived?.Invoke(new UserDataDto());
        }

#endif
        
    }
}