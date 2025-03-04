using System.Collections.Generic;
using FirebaseCore.DTOs;
using Newtonsoft.Json;
using UnityEngine;
using System;

#if UNITY_WEBGL && !UNITY_EDITOR
using FirebaseWebGL.Scripts.FirebaseBridge;
#else
using Cysharp.Threading.Tasks;
using Firebase.Database;
using Firebase;
#endif

namespace FirebaseCore.Listeners
{
    public abstract class FirebaseListener<TDto> where TDto : struct 
    {
        protected readonly string Room;

        public Action<TDto> OnDataReceived;
        
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
        
        public void Disconnect()
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

            Connect().Forget();
        }

        private async UniTaskVoid Connect()
        {
            // Initialize Firebase
            await FirebaseApp.CheckAndFixDependenciesAsync();
            
            GetReference();
            
            ListenToChanges();
        }

        protected abstract void GetReference();

        private void ListenToChanges()
        {
            Reference.ChildAdded += HandleChildChanged;
            Reference.ChildChanged += HandleChildChanged;
        }

        protected abstract void HandleChildChanged(object sender, ChildChangedEventArgs e);

        public void Disconnect()
        {
            if (Reference == null)
                return;
            
            Reference.ChildAdded -= HandleChildChanged;
            Reference.ChildChanged -= HandleChildChanged;
        }
#endif

        protected static T ConvertTo<T>(Dictionary<string, object> data)
        {
            return ConvertTo<T>(JsonConvert.SerializeObject(data));
        }
        
        protected static T ConvertTo<T>(string obj)
        {
            return JsonConvert.DeserializeObject<T>(obj);
        }

    }
}