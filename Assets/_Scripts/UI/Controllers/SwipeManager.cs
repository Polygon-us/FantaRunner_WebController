using ThisOtherThing.UI.Shapes;
using FirebaseCore.Senders;
using UnityEngine;

public enum SwipeDirection
{
    None,
    Up,
    Down,
    Left,
    Right
}

namespace UI.Controllers
{
    public class SwipeManager : ControllerBase
    {
        [SerializeField] private float minSwipeDistance = 10f;
        [Space]
        [SerializeField] private Polygon upPolygon;
        [SerializeField] private Polygon downPolygon;
        [SerializeField] private Polygon leftPolygon;
        [SerializeField] private Polygon rightPolygon;

        private Vector2 startingTouch = Vector2.zero;
        private SwipeDirection direction;
        private bool isSwiping;
        
        private TouchControls inputActions;
        
        private DirectionSender directionSender;

        public override void OnCreation(RoomConfig roomConfig)
        {
            base.OnCreation(roomConfig);
            
            inputActions = new TouchControls();
            inputActions.Enable();
            
            directionSender = new DirectionSender(roomConfig.roomName);
        }

        public override void OnShow()
        {
            ResetShadows();
        }

        // private void OnEnable()
        // {
        //     inputActions.Main.Touch.performed += ProcessTouch;
        // }
        //
        // private void OnDisable()
        // {
        //     inputActions.Main.Touch.performed -= ProcessTouch;
        // }

        private void Update()
        {
            if (inputActions.Main.Touch.WasPerformedThisFrame())
            {
                isSwiping = true;
                direction = SwipeDirection.None;
                startingTouch = inputActions.Main.Swipe.ReadValue<Vector2>();
                ResetShadows();
            }

            if (inputActions.Main.Touch.WasReleasedThisFrame())
            {
                isSwiping = false;
            }

            if (isSwiping)
            {
                Vector2 diff = inputActions.Main.Swipe.ReadValue<Vector2>() - startingTouch;
                
                diff = new Vector2(diff.x / Screen.width, diff.y / Screen.width);

                if (diff.magnitude > 0.05f)
                {
                    direction = GetSwipeDirection(diff);

                    directionSender.Send(direction);
                    UpdateShadows(direction);
                   
                    isSwiping = false;
                }
            }
        }

        private SwipeDirection GetSwipeDirection(Vector2 swipeDirection)
        {
            if (Mathf.Abs(swipeDirection.x) < Mathf.Abs(swipeDirection.y))
            {
                return swipeDirection.y > 0 ? SwipeDirection.Up : SwipeDirection.Down;
            }
            else
            {
                return swipeDirection.x < 0 ? SwipeDirection.Left : SwipeDirection.Right;
            }
        }

        private void UpdateShadows(SwipeDirection direction)
        {
            upPolygon.ShadowProperties.ShowShadows = direction == SwipeDirection.Up;
            downPolygon.ShadowProperties.ShowShadows = direction == SwipeDirection.Down;
            leftPolygon.ShadowProperties.ShowShadows = direction == SwipeDirection.Left;
            rightPolygon.ShadowProperties.ShowShadows = direction == SwipeDirection.Right;

            UiMeshesUpdate();
        }

        private void UiMeshesUpdate()
        {
            upPolygon.ForceMeshUpdate();
            downPolygon.ForceMeshUpdate();
            leftPolygon.ForceMeshUpdate();
            rightPolygon.ForceMeshUpdate();
        }

        private void ResetShadows()
        {
            upPolygon.ShadowProperties.ShowShadows = false;
            downPolygon.ShadowProperties.ShowShadows = false;
            leftPolygon.ShadowProperties.ShowShadows = false;
            rightPolygon.ShadowProperties.ShowShadows = false;

            UiMeshesUpdate();
        }

        public override void OnHide()
        {
            directionSender.Delete();
        }
    }
}