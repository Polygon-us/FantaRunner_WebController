using FirebaseWebGL.Scripts.FirebaseBridge;
using System.Collections.Generic;
using DTOs.Firebase;
using UnityEngine;

public class FirestoreSender : MonoBehaviour
{
    private int counter;

    private readonly Dictionary<SwipeDirection, int> values = new()
    {
        {SwipeDirection.None, 0},
        {SwipeDirection.Up, 1},
        {SwipeDirection.Down, 2},
        {SwipeDirection.Left, 3},
        {SwipeDirection.Right, 4}
    };

    private const string Document = "A1B1";

    private DirectionDto directionData;

    private static FirestoreSender _instance;

    public static FirestoreSender Instance
    {
        get
        {
            if (_instance)
                return _instance;

            _instance = FindAnyObjectByType<FirestoreSender>();

            if (!_instance)
                _instance = new GameObject("FirestoreSender").AddComponent<FirestoreSender>();

            DontDestroyOnLoad(_instance);
            
            return _instance;
        }
    }

    public void SendUser(RegisterDto registerDto)
    {
        FirebaseDatabase.PostJSON
        (
            null,
            JsonUtility.ToJson(registerDto),
            gameObject.name,
            nameof(OnRequestSuccess),
            nameof(OnRequestFail)
        );
    }

    public void SendDirection(SwipeDirection direction)
    {
        FirebaseDatabase.UpdateJSON(Document, GetDirectionJson(values[direction], counter++));
    }

    private string GetDirectionJson(int direction, int count)
    {
        directionData.direction = direction;
        directionData.count = count;

        return JsonUtility.ToJson(directionData);
    }

    private void OnRequestSuccess(string message)
    {
        Debug.Log(message);
    }

    private void OnRequestFail(string message)
    {
        Debug.LogError(message);
    }
}