namespace FirebaseCore.DTOs
{
    public struct GameStateDto
    {
        public GameStates state;
    }
    
    public enum GameStates
    {
        Register,
        Game,
        End
    }
}