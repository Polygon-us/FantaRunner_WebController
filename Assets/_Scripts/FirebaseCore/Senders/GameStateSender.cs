using DTOs.Firebase;
using FirebaseCore.DTOs;
using UnityEngine;

#if UNITY_WEBGL && !UNITY_EDITOR
using FirebaseWebGL.Scripts.FirebaseBridge;
#else
using Firebase.Database;
#endif

namespace FirebaseCore.Senders
{
    public class GameStateSender : FirebaseSender<GameStateDto>
    {
        private const string GameStateChild = "gameState";

        public GameStateSender(string room) : base(room)
        {
        }

#if UNITY_WEBGL && !UNITY_EDITOR  
        public override void Send(GameStateDto stateDto)
        {
            FirebaseDatabase.PostJSON
            (
                $"{Room}/{GameStateChild}",
                JsonUtility.ToJson(stateDto),
                FirebaseReceiver.Instance.Name,
                FirebaseReceiver.Instance.SuccessCallback,
                FirebaseReceiver.Instance.FailCallback
            );
        }
#else
        public override void Send(GameStateDto stateDto)
        {
            Reference.Child(GameStateChild).SetRawJsonValueAsync(JsonUtility.ToJson(stateDto));
        }
#endif
    }
}