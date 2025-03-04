#if UNITY_WEBGL && !UNITY_EDITOR 

#else
using Firebase.Database;
#endif
using FirebaseCore.DTOs;
using UnityEngine;

namespace FirebaseCore.Listeners
{
    public class DirectionListener : FirebaseListener<UserInputDto>
    {
        private const string DirectionCollection = "direction";
        
        public DirectionListener(string room) : base(room)
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
            Reference = FirebaseDatabase.DefaultInstance.GetReference($"{Room}/{DirectionCollection}");
        }

        protected override void HandleChildChanged(object sender, ChildChangedEventArgs e)
        {
            // OnDataReceived?.Invoke(ConvertTo<UserInputDto>(e.Snapshot.Value));
            
            Debug.Log("Child changed/added: " + e.Snapshot.Key + " " + e.Snapshot.Value);
        }

#endif
        
    }
}