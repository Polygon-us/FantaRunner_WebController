using System;
using UnityEngine;

namespace FirebaseCore
{
    public class FirebaseReceiver : MonoBehaviour
    {
        private static FirebaseReceiver _instance;

        public static FirebaseReceiver Instance
        {
            get
            {
                if (_instance)       
                    return _instance;
                
                _instance = FindAnyObjectByType<FirebaseReceiver>();
                
                if (!_instance)
                    _instance = new GameObject("FirebaseReceiver").AddComponent<FirebaseReceiver>();
                
                return _instance;
            }
        }
        
        public string Name => _instance.gameObject.name;

        public string SuccessCallback => nameof(OnRequestSuccess);
        public string FailCallback => nameof(OnRequestFail);
        
        public string ChildChangedCallback => nameof(OnChildChanged);
        public string ChildAddedCallback => nameof(OnChildAdded);
        
        public Action<string> ChildChanged;
        public Action<string> ChildAdded;
        
        private void OnRequestSuccess(string message)
        {
            Debug.Log(message);
        }

        private void OnRequestFail(string message)
        {
            Debug.LogError(message);
        }

        private void OnChildChanged(string data) => ChildChanged?.Invoke(data);
        private void OnChildAdded(string data) => ChildAdded?.Invoke(data);
    }
}