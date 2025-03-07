using DTOs.Firebase;
using Newtonsoft.Json;

#if FIREBASE_WEB
using FirebaseCore.Receivers;
using FirebaseWebGL.Scripts.FirebaseBridge;
#else
using Firebase.Database;
#endif

namespace FirebaseCore.Senders
{
    public class UserDatabaseSender : FirebaseSender<RegisterDto>
    {
        protected override string ChildName { get; set; } = "allUsers";

        public UserDatabaseSender(string room) : base(room)
        {
        }

#if FIREBASE_WEB  
        public override void Send(RegisterDto registerDto)
        {
            Receiver receiver = ReceiverManager.Instance.Register(GetType());
            
            FirebaseDatabase.UpdateJSON
            (
                $"{Room}/{ChildName}/{registerDto.username}",
                JsonConvert.SerializeObject(registerDto),
                receiver.Name,
                receiver.SuccessCallback,
                receiver.FailCallback
            );
        }
#else
        public override void Send(RegisterDto registerDto)
        {
            Reference.Child(registerDto.username).SetRawJsonValueAsync(JsonConvert.SerializeObject(registerDto));
        }
#endif
    }
}