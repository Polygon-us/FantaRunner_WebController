using FirebaseCore.DTOs;
using Newtonsoft.Json;
using UnityEngine;
using System.Collections.Generic;
using DTOs.Firebase;

#if UNITY_WEBGL && !UNITY_EDITOR
using FirebaseWebGL.Scripts.FirebaseBridge;
#else
using Firebase.Extensions;
using Firebase.Database;
using Firebase;
#endif

namespace FirebaseCore.Senders
{
    public class DirectionSender : FirebaseSender<SwipeDirection>
    {
        private int counter;

        protected override string ChildName { get; set; } = "movement";

        private readonly Dictionary<SwipeDirection, int> values = new()
        {
            {SwipeDirection.None, 0},
            {SwipeDirection.Up, 1},
            {SwipeDirection.Down, 2},
            {SwipeDirection.Left, 3},
            {SwipeDirection.Right, 4}
        };

        private DirectionDto directionData;


        public DirectionSender(string room) : base(room)
        {
            counter = 0;
            directionData = new DirectionDto();
        }

#if UNITY_WEBGL && !UNITY_EDITOR
        public override void Send(SwipeDirection direction)
        {
            FirebaseDatabase.UpdateJSON
            (
                $"{Room}/{ChildName}",
                GetDirectionJson(values[direction], counter++),
                FirebaseReceiver.Instance.Name,
                FirebaseReceiver.Instance.SuccessCallback,
                FirebaseReceiver.Instance.FailCallback
            );
        }
#else
        public override void Send(SwipeDirection direction)
        {
            Reference.SetRawJsonValueAsync(GetDirectionJson(values[direction], counter++));
        }
#endif
        
        private string GetDirectionJson(int direction, int count)
        {
            directionData.direction = direction;
            directionData.count = count;

            return JsonUtility.ToJson(directionData);
        }
    }
}