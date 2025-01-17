using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class FirestoreSender : MonoBehaviour
{
    public static Action<SwipeDirection> directionToSend;

    [SerializeField, TextArea] private string baseUrl;

    private const string _put = "PUT";

    private readonly Dictionary<SwipeDirection, byte[]> values = new()
    {
        { SwipeDirection.None, new byte[] { 48 } },
        { SwipeDirection.Up, new byte[] { 49 } },
        { SwipeDirection.Down, new byte[] { 50 } },
        { SwipeDirection.Left, new byte[] { 51 } },
        { SwipeDirection.Right, new byte[] { 52 } },
    };

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
        UnityWebRequest webRequest = new(baseUrl, _put)
        {
            uploadHandler = new UploadHandlerRaw(values[direction])
        };

        webRequest.SendWebRequest();
    }
}