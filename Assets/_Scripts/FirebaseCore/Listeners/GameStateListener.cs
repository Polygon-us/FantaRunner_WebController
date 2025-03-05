#if UNITY_WEBGL && !UNITY_EDITOR 
#else
using Firebase.Database;
#endif
using FirebaseCore.DTOs;
using UnityEngine;

namespace FirebaseCore.Listeners
{
    public class GameStateListener : FirebaseListener<GameStateDto>
    {
        protected override string ChildName { get; set; } = "gameState";
        
        public GameStateListener(string room) : base(room)
        {
        }
#if UNITY_WEBGL && !UNITY_EDITOR 
        protected override void HandleValueChanged(string data)
        {   
            Debug.Log(data);
        }
#else

        protected override void HandleChildChanged(object sender, ChildChangedEventArgs e)
        {
            GameStateDto gameStateDto = new GameStateDto
            {
                state = (GameStates)e.Snapshot.Value
            };
            Debug.Log("Changed: " + e.Snapshot.Value);
            
            OnDataReceived?.Invoke(gameStateDto);
        }
#endif
    }
}