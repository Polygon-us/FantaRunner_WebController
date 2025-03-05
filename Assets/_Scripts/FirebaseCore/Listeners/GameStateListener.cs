using Firebase.Database;
using FirebaseCore.DTOs;

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
            
        }
    }
}