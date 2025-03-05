using DTOs.Firebase;
using FirebaseCore.DTOs;
using UnityEngine;

#if UNITY_WEBGL && !UNITY_EDITOR
using FirebaseWebGL.Scripts.FirebaseBridge;
#else
using Firebase.Database;
using Newtonsoft.Json;
#endif

namespace FirebaseCore.Senders
{
    public class GameStateSender : FirebaseSender<GameStateDto>
    {
        protected override string ChildName { get; set; } = "gameState";

        public GameStateSender(string room) : base(room)
        {
        }

#if UNITY_WEBGL && !UNITY_EDITOR  
        public override void Send(GameStateDto stateDto)
        {
            FirebaseDatabase.PostJSON
            (
                $"{Room}/{ChildName}",
                sonConvert.SerializeObject(stateDto),
                FirebaseReceiver.Instance.Name,
                FirebaseReceiver.Instance.SuccessCallback,
                FirebaseReceiver.Instance.FailCallback
            );
        }
#else
        public override void Send(GameStateDto stateDto)
        {
            string json = JsonConvert.SerializeObject(stateDto);
            Reference.SetRawJsonValueAsync(json);
        }
#endif
    }
}