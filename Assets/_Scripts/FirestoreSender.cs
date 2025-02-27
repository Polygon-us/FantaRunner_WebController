using FirebaseWebGL.Scripts.FirebaseBridge;
using System.Collections.Generic;
using DTOs.Firebase;
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
    
    private const string Document = "A1B1";
    
    private DirectionDto directionData;

    private void OnEnable()
    {
        DirectionToSend += SendDirection;
    }

    private void OnDisable()
    {
        DirectionToSend -= SendDirection;
    }
 
    private void SendDirection(SwipeDirection direction)
    {
        FirebaseDatabase.UpdateJSON(Document, GetJson(values[direction], counter++));
    }

    private string GetJson(int direction, int count)
    {
        directionData.direction = direction;
        directionData.count = count;
        
        return JsonUtility.ToJson(directionData);
    }
}
