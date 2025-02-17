using FirebaseWebGL.Scripts.FirebaseBridge;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FirestoreSender : MonoBehaviour
{
    public static Action<SwipeDirection> DirectionToSend;

    private int counter = 0;

    private readonly Dictionary<SwipeDirection, int> values = new()
    {
        { SwipeDirection.None, 0 },
        { SwipeDirection.Up, 1 },
        { SwipeDirection.Down, 2 },
        { SwipeDirection.Left, 3 },
        { SwipeDirection.Right, 4 }
    };
    
    private const string _document = "A1B1";
    
    private DirectionData _directionData;

    private void Awake()
    {
        DirectionToSend += SendDirection;
    }

    private void OnDestroy()
    {
        DirectionToSend -= SendDirection;
    }
 
    private void SendDirection(SwipeDirection direction)
    {
        FirebaseDatabase.UpdateJSON(_document, GetJson(values[direction], counter++));
    }

    private string GetJson(int direction, int count)
    {
        _directionData.direction = direction;
        _directionData.count = count;
        
        return JsonUtility.ToJson(_directionData);
    }
}

public struct DirectionData
{
    public int direction;
    public int count;
}