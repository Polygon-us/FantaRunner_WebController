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
#else
        protected DatabaseReference Reference;

        protected FirebaseSender(string room)
        {
            Room = room;

            GetReference();
        }

        public void GetReference()
        {
            Reference = FirebaseDatabase.DefaultInstance.GetReference(Room).Child(ChildName);
        }
#endif

        public abstract void Send(T obj);

        public void Delete()
        {
            Reference.RemoveValueAsync();
        }
    }
}