using Firebase.Database;
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

        protected override void HandleChildChanged(object sender, ChildChangedEventArgs e)
        {
            GameStateDto gameStateDto = new GameStateDto
            {
                state = (GameStates)e.Snapshot.Value
            };
            Debug.Log("Changed: " + e.Snapshot.Value);
            
            OnDataReceived?.Invoke(gameStateDto);
        }
    }
}