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

namespace FirebaseCore.Listeners
{
    public class FirebaseConnection : MonoBehaviour
    {
        private const string Room = "A1B1";

        public static Action<int> OnMovementInput;

#if UNITY_WEBGL && !UNITY_EDITOR
        private void Start()
        {
            ListenToDatabaseChanges();
        }

        public void ListenToDatabaseChanges()
        {
            FirebaseDatabase.ListenForChildChanged(Room, gameObject.name, nameof(HandleValueChanged), nameof(HandleError));
        }

        private void HandleValueChanged(string data)
        {
            ChangedDataDto dataDto = ConvertTo<ChangedDataDto>(data);
            
            switch (dataDto.key)
            {
                case nameof(UserInputDto.direction):
                    OnMovementInput?.Invoke(int.Parse(dataDto.value));
                    break;
            }

        }

        private void HandleError(string error)
        {
            Debug.LogError(error);
        }

        private void OnDisable()
        {
            FirebaseDatabase.StopListeningForChildChanged(Room, gameObject.name, nameof(HandleValueChanged),
                nameof(HandleError));
        }

#else
        private DatabaseReference reference;

        private void Start()
        {
            // Initialize Firebase
            FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
            {
                if (task.IsCompleted)
                {
                    reference = FirebaseDatabase.DefaultInstance.GetReference(Room);

                    // Set up a listener for changes
                    reference.ChildChanged += HandleChildChanged;
                    reference.ChildAdded += HandleChildAdded;
                }
                else
                {
                    Debug.LogError("Could not resolve Firebase dependencies: " + task.Exception);
                }
            });
        }

        private void HandleChildChanged(object sender, ChildChangedEventArgs e)
        {
            if (e.DatabaseError != null)
            {
                Debug.LogError("Error: " + e.DatabaseError.Message);
                return;
            }

            // Log the changed child key and its new value
            Debug.Log("Child changed: " + e.Snapshot.Key + " -> " + e.Snapshot.Value);
        }

        private void HandleChildAdded(object sender, ChildChangedEventArgs e)
        {
            if (e.DatabaseError != null)
            {
                Debug.LogError("Error: " + e.DatabaseError.Message);
                return;
            }
            
            // Log the changed child key and its new value
            Debug.Log("Child Added: " + e.Snapshot.Key + " -> " + e.Snapshot.Value);
        }

        private void OnDisable()
        {
            reference.ChildChanged -= HandleChildChanged;
            reference.ChildAdded -= HandleChildAdded;
        }
#endif
        
        private static T ConvertTo<T>(string obj)
        {
            return JsonConvert.DeserializeObject<T>(obj);
        }

    }
}