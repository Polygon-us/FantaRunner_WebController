using FirebaseCore.DTOs;
using Newtonsoft.Json;
using UnityEngine;
using System;

#if UNITY_WEBGL && !UNITY_EDITOR
using FirebaseWebGL.Scripts.FirebaseBridge;
#else
using Firebase.Extensions;
using Firebase.Database;
using Firebase;
#endif


namespace FirebaseCore.Senders
{
    public abstract class FirestoreSender<T>
    {
        protected readonly string Room;
        
#if UNITY_WEBGL && !UNITY_EDITOR        

        protected FirestoreSender(string room)
        {
            Room = room;
        }
#else
        protected DatabaseReference Reference;
        
        protected FirestoreSender(string room)
        {
            Room = room;
            
            FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
            {
                if (task.IsCompleted)
                {
                    Reference = FirebaseDatabase.DefaultInstance.GetReference(Room);
                }
                else
                {
                    Debug.LogError("Could not resolve Firebase dependencies: " + task.Exception);
                }
            });
#endif
        }
        
        public abstract void Send(T obj);
    }
}