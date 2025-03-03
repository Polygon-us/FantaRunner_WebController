namespace FirebaseCore.Senders
{
    public abstract class FirestoreSender<T>
    {
        protected readonly string Room;

        protected FirestoreSender(string room)
        {
            Room = room;
        }
        
        public abstract void Send(T obj);
    }
}