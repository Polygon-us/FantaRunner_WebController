using FirebaseCore.DTOs;
using Newtonsoft.Json;
using UnityEngine;
using System;

#if UNITY_WEBGL && !UNITY_EDITOR
using FirebaseWebGL.Scripts.FirebaseBridge;
#else
using Firebase.Database;
#endif


namespace FirebaseCore.Senders
{
    public abstract class FirebaseSender<T>
    {
        protected readonly string Room;
        protected abstract string ChildName { get; set; }

#if UNITY_WEBGL && !UNITY_EDITOR
        protected FirebaseSender(string room)
        {
            Room = room;
        }

        public void Delete()
        {
            FirebaseDatabase.DeleteJSON
            (
                $"{Room}/{ChildName}",
                FirebaseReceiver.Instance.Name,
                FirebaseReceiver.Instance.SuccessCallback,
                FirebaseReceiver.Instance.FailCallback
            );
        }
#else
        protected DatabaseReference Reference;

        protected FirebaseSender(string room)
        {
            Room = room;

            GetReference();
        }

        private void GetReference()
        {
            Reference = FirebaseDatabase.DefaultInstance.GetReference(Room).Child(ChildName);
        }
        
        public void Delete()
        {
            Reference.RemoveValueAsync();
        }
#endif

        public abstract void Send(T obj);

    }
}