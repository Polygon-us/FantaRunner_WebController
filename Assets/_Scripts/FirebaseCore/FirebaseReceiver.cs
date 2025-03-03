using UnityEngine;

namespace FirebaseCore
{
    public class FirebaseReceiver : MonoBehaviour
    {
        private static FirebaseReceiver _instance;
        
        public static FirebaseReceiver Instance => _instance;
        
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