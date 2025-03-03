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
        
        private void OnRequestSuccess(string message)
        {
            Debug.Log(message);
        }

        private void OnRequestFail(string message)
        {
            Debug.LogError(message);
        }
    }
}