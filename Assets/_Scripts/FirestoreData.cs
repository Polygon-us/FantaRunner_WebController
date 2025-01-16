using System;

[Serializable]
public class FirestoreData
{
    public Fields fields;

    [Serializable]
    public class Fields
    {
        public Direction direction;
    }

    [Serializable]
    public class Direction
    {
        public string integerValue;
    }
}