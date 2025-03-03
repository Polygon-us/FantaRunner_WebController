using FirebaseCore.DTOs;
using Newtonsoft.Json;
using UnityEngine;
using System;

#if UNITY_WEBGL && !UNITY_EDITOR
using FirebaseWebGL.Scripts.FirebaseBridge;
#else
using System.Threading.Tasks;
using Firebase.Database;
using Firebase;
#endif

namespace FirebaseCore.Listeners
{
    public abstract class FirebaseListener
    {
        protected readonly string Room;
        
#if UNITY_WEBGL && !UNITY_EDITOR
        protected FirebaseListener(string room)
        {
            Room = room;

            ListenToDatabaseChanges();
        }

        public void ListenToDatabaseChanges()
        {
            FirebaseReceiver receiver = FirebaseReceiver.Instance;
            FirebaseDatabase.ListenForChildChanged(Room, receiver.Name, receiver.ChildChangedCallback, receiver.FailCallback);
            FirebaseDatabase.ListenForChildAdded(Room, receiver.Name, receiver.ChildAddedCallback, receiver.FailCallback);
            FirebaseReceiver.Instance.ChildChanged = HandleValueChanged;
            FirebaseReceiver.Instance.ChildAdded = HandleValueChanged;
        }

        protected abstract void HandleValueChanged(string data);
        
        public void StopListening()
        {
            FirebaseReceiver receiver = FirebaseReceiver.Instance;
            FirebaseDatabase.StopListeningForChildChanged(Room, receiver.Name, receiver.ChildChangedCallback, receiver.FailCallback);
            FirebaseDatabase.StopListeningForChildAdded(Room, receiver.Name, receiver.ChildAddedCallback, receiver.FailCallback);
        }

#else
        protected DatabaseReference Reference;

        protected FirebaseListener(string room)
        {
            Room = room;

            Connect();
        }

        protected virtual async Task Connect()
        {
            // Initialize Firebase
            await FirebaseApp.CheckAndFixDependenciesAsync();
        }
        
#endif
        
        private static T ConvertTo<T>(string obj)
        {
            return JsonConvert.DeserializeObject<T>(obj);
        }

    }
}