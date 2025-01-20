using FirebaseWebGL.Scripts.FirebaseBridge;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class FirestoreSender : MonoBehaviour
{
    public static Action<SwipeDirection> directionToSend;

    [SerializeField, TextArea] private string baseUrl;
    private readonly Dictionary<SwipeDirection, int> values = new()
    {
        { SwipeDirection.None, 0 },
        { SwipeDirection.Up, 1 },
        { SwipeDirection.Down, 2 },
        { SwipeDirection.Left, 3 },
        { SwipeDirection.Right, 4 },
    };

    private const string _document = "A1B1";

    private int counter = 0;

    private readonly StringBuilder _jsonBuilder = new(64);

    private void Awake()
    {
        directionToSend += SendDirection;
    }

    private void OnDestroy()
    {
        directionToSend -= SendDirection;
    }

    private void SendDirection(SwipeDirection direction)
    {
        FirebaseDatabase.UpdateJSON(_document, GetJson(values[direction], counter++));
    }
    private string GetJson(int direction, int count)
    {
        _jsonBuilder.Clear();
        _jsonBuilder.Append("{\"direction\": ");
        _jsonBuilder.Append(direction);
        _jsonBuilder.Append(", \"count\": ");
        _jsonBuilder.Append(count);
        _jsonBuilder.Append("}");
        return _jsonBuilder.ToString();
    }
}