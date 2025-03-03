using FirebaseWebGL.Scripts.FirebaseBridge;
using System.Collections.Generic;
using DTOs.Firebase;
using UnityEngine;

namespace FirebaseCore.Senders
{
    public class DirectionSender : FirestoreSender<SwipeDirection>
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

        private DirectionDto directionData;


        public DirectionSender(string room) : base(room)
        {
            counter = 0;
            directionData = new DirectionDto();
        }

        public override void Send(SwipeDirection direction)
        {
            FirebaseDatabase.UpdateJSON
            (
                $"{Room}/direction",
                GetDirectionJson(values[direction], counter++),
                FirebaseReceiver.Instance.Name,
                FirebaseReceiver.Instance.SuccessCallback,
                FirebaseReceiver.Instance.FailCallback
            );
        }

        private string GetDirectionJson(int direction, int count)
        {
            directionData.direction = direction;
            directionData.count = count;

            return JsonUtility.ToJson(directionData);
        }
    }
}