using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using System.Net.Http;

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
        //SendUnityWebRequest(string.Format(_json, values[direction])).Forget();
        SendHttpClient(string.Format(_json, values[direction])).Forget();
    }

    private async UniTaskVoid SendUnityWebRequest(string json)
    {
        UnityWebRequest webRequest = new(baseUrl, _patch)
        {
            uploadHandler = new UploadHandlerRaw(Encoding.UTF8.GetBytes(json))
        };

        await webRequest.SendWebRequest();
    }

    private readonly HttpClient client = new();
    private readonly HttpMethod method = new(_patch);
    private HttpRequestMessage request;
    private const string _mediaType = "application/json";

    private async UniTaskVoid SendHttpClient(string json)
    {
        request = new(method, baseUrl)
        {
            Content = new StringContent(json, Encoding.UTF8, _mediaType)
        };

        await client.SendAsync(request);
    }
}