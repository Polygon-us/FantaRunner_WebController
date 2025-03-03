using FirebaseWebGL.Scripts.FirebaseBridge;
using DTOs.Firebase;
using UnityEngine;

namespace FirebaseCore.Senders
{
    public class UserSender : FirestoreSender<RegisterDto>
    {
        public UserSender(string room) : base(room)
        {
        }

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
    }
}