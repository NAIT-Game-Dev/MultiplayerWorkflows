using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEditor.Timeline.TimelinePlaybackControls;

public class W2_PlayerMovement : MonoBehaviour
{
    float movementSpeed = 3.0f;

    [SerializeField] int gamepadID, playerIndex;

    [SerializeField] InputAction moveAction;

    [SerializeField] Vector2 moveValue;

    float xValue, zValue;

    // Update is called once per frame
    void Update()
    {
        if (gamepadID != -1)
        {
            #region Controller Input for Gamepad
          
            //moveValue = moveAction.ReadValue<Vector2>();

            #endregion
        }
    }

    private void FixedUpdate()
    {
        // move the object based on the values of the gamepad
        transform.Translate(new Vector3(moveValue.x * movementSpeed * Time.fixedDeltaTime, 0, moveValue.y * movementSpeed * Time.fixedDeltaTime));
    }

    public int GetGamepadID()
    {
        // return the gamepad ID that the object is currently linked to.
        return gamepadID;
    }

    public void SetGamepadID(int id)
    {
        // set the gamepad ID that the object should be linked to.
        gamepadID = id;
    }

    private void OnEnable()
    {
        moveAction.Enable();
    }

    private void OnDisable()
    {
        moveAction.Disable();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveValue = context.ReadValue<Vector2>();
    }

    //public void OnMove(InputValue value)
    //{
    //    moveValue = value.Get<Vector2>();
    //}
}