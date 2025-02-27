using FirebaseWebGL.Scripts.FirebaseBridge;
using System.Collections.Generic;
using DTOs.Firebase;
using UnityEngine;
using System;

public static class FirestoreSender
{
    private static int _counter;

    private static readonly Dictionary<SwipeDirection, int> Values = new()
    {
        { SwipeDirection.None, 0 },
        { SwipeDirection.Up, 1 },
        { SwipeDirection.Down, 2 },
        { SwipeDirection.Left, 3 },
        { SwipeDirection.Right, 4 }
    };
    
    private const string Document = "A1B1";
    
    private static DirectionDto _directionData;

    [RuntimeInitializeOnLoadMethod]
    private static void InitializeOnLoad()
    {
        _counter = 0;    
        _directionData = new DirectionDto();
    }
 
    public static void SendDirection(SwipeDirection direction)
    {
        FirebaseDatabase.UpdateJSON(Document, GetDirectionJson(Values[direction], _counter++));
    }

    private static string GetDirectionJson(int direction, int count)
    {
        _directionData.direction = direction;
        _directionData.count = count;
        
        return JsonUtility.ToJson(_directionData);
    }
}
