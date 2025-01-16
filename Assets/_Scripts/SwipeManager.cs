using ThisOtherThing.UI.Shapes;
using UnityEngine;
using UnityEngine.InputSystem;

public enum SwipeDirection
{
    None,
    Up,
    Down,
    Left,
    Right
}

public class SwipeManager : MonoBehaviour
{
    private TouchControls inputActions;

    [SerializeField] private float minSwipeDistance = 10f;

    [Space]
    [SerializeField] private Polygon upPolygon;
    [SerializeField] private Polygon downPolygon;
    [SerializeField] private Polygon leftPolygon;
    [SerializeField] private Polygon rightPolygon;

    private Vector2 swipeDirection = Vector2.zero;
    private SwipeDirection direction;

    private void Awake()
    {
        inputActions = new TouchControls();
        inputActions.Enable();
    }

    private void OnEnable()
    {
        inputActions.Main.Swipe.performed += ProcessSwipe;
        inputActions.Main.Touch.canceled += ProcessTouch;
    }

    private void OnDisable()
    {
        inputActions.Main.Swipe.performed -= ProcessSwipe;
        inputActions.Main.Touch.canceled -= ProcessTouch;
    }

    private void ProcessSwipe(InputAction.CallbackContext ctx)
    {
        swipeDirection = ctx.ReadValue<Vector2>();
    }

    private void ProcessTouch(InputAction.CallbackContext context)
    {
        if (Mathf.Abs(swipeDirection.magnitude) < minSwipeDistance)
        {
            direction = SwipeDirection.None;
            ResetShadows();
            return;
        }

        direction = GetSwipeDirection(swipeDirection);
        FirestoreSender.directionToSend?.Invoke(direction);

        UpdateShadows(direction);
        UiMeshesUpdate();
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
    }
}