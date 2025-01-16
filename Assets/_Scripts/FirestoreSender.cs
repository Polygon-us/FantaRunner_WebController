using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

public class FirestoreSender : MonoBehaviour
{
    public static Action<SwipeDirection> directionToSend;

    [SerializeField, TextArea] private string baseUrl;

    private const string _patch = "PATCH";
    private const string _json = @"
    {{
        ""fields"": {{
            ""direction"": {{
                ""integerValue"": ""{0}""
            }}
        }}
    }}";

    private readonly Dictionary<SwipeDirection, string> values = new()
    {
        { SwipeDirection.None, "0" },
        { SwipeDirection.Up, "1" },
        { SwipeDirection.Down, "2" },
        { SwipeDirection.Left, "3" },
        { SwipeDirection.Right, "4" },
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
        SendUnityWebRequest(string.Format(_json, values[direction])).Forget();
    }

    private async UniTaskVoid SendUnityWebRequest(string json)
    {
        UnityWebRequest webRequest = new(baseUrl, _patch)
        {
            uploadHandler = new UploadHandlerRaw(Encoding.UTF8.GetBytes(json))
        };

        await webRequest.SendWebRequest();
    }
}