using DTOs.Firebase;
using UnityEngine;

#if UNITY_WEBGL && !UNITY_EDITOR
using FirebaseWebGL.Scripts.FirebaseBridge;
#else
using Firebase.Database;
#endif

namespace FirebaseCore.Senders
{
    public class UserSender : FirebaseSender<RegisterDto>
    {
        private const string UserChild = "user";

        public UserSender(string room) : base(room)
        {
        }

#if UNITY_WEBGL && !UNITY_EDITOR  
        public override void Send(RegisterDto registerDto)
        {
            FirebaseDatabase.PostJSON
            (
                $"{Room}/user",
                JsonUtility.ToJson(registerDto),
                FirebaseReceiver.Instance.Name,
                FirebaseReceiver.Instance.SuccessCallback,
                FirebaseReceiver.Instance.FailCallback
            );
        }
#else
        public override void Send(RegisterDto registerDto)
        {
            Reference.Child(UserChild).SetRawJsonValueAsync(JsonUtility.ToJson(registerDto));
        }
#endif
    }
}