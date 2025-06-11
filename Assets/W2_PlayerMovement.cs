using UnityEngine;
using UnityEngine.InputSystem;

public class W2_PlayerMovement : MonoBehaviour
{
    float movementSpeed = 3.0f;

    [SerializeField] int gamepadID, playerIndex;

    float forward, backward, right, left, xValue, zValue;

    // Update is called once per frame
    void Update()
    {
        if (gamepadID != -1)
        {
            #region Controller Input for Gamepad
            // playerID is the index of the gamepad in the Gamepad list. Make sure the playerID does not exceed the number of gamepads plugged in.

            // use the controls of the gamepad in the list based on the playerID index. In this example we are getting the float values of the left stick controls.
            forward = Gamepad.all[playerIndex].leftStick.up.value;
            backward = Gamepad.all[playerIndex].leftStick.down.value;
            right = Gamepad.all[playerIndex].leftStick.right.value;
            left = Gamepad.all[playerIndex].leftStick.left.value;

            xValue = right - left;
            zValue = forward - backward;
            #endregion
        }
    }

    private void FixedUpdate()
    {
        // move the object based on the values of the gamepad
        transform.Translate(new Vector3(xValue * movementSpeed * Time.fixedDeltaTime, 0, zValue * movementSpeed * Time.fixedDeltaTime));
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
}