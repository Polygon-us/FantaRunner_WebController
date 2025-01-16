using Cysharp.Threading.Tasks;
using System;
using UnityEngine;
using UnityEngine.Networking;

public class FirestoreSender : MonoBehaviour
{
    public static Action<SwipeDirection> directionToSend;

    [SerializeField, TextArea] private string baseUrl;
    [SerializeField] private Document session = new();

    private int value = 0;

    private const string _patch = "PATCH";

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
        value = (int)direction;
        session.fields.direction.integerValue = value.ToString();
        SendDirectionToFirestore(JsonUtility.ToJson(session)).Forget();
    }

    private async UniTaskVoid SendDirectionToFirestore(string json)
    {
        using UnityWebRequest webRequest = UnityWebRequest.Put(baseUrl, json);
        webRequest.method = _patch;
        await webRequest.SendWebRequest();
    }
}

[Serializable]
public class Document
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