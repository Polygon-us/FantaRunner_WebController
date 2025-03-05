#if UNITY_WEBGL && !UNITY_EDITOR 

#else
using Firebase.Database;
#endif
using FirebaseCore.DTOs;
using UnityEngine;

namespace FirebaseCore.Listeners
{
    public class ResultListener : FirebaseListener<UserResultDto>
    {
        protected override string ChildName { get; set; } = "direction";

        public ResultListener(string room) : base(room)
        {
        }
        
#if UNITY_WEBGL && !UNITY_EDITOR 
        protected override void HandleValueChanged(string data)
        {   
            Debug.Log(data);
        }
#else
        
        protected override void HandleChildChanged(object sender, ChildChangedEventArgs e)
        {
            Debug.Log("Child changed/added: " + e.Snapshot.Key + " " + e.Snapshot.Value);
        }

#endif
        
    }
}