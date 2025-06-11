using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Lobby : MonoBehaviour
{
    [SerializeField] GamepadManager gamepadManager;
    [SerializeField] List<TMP_Text> playerText;

    // Start is called before the first frame update
    void Start()
    {
        gamepadManager = GameObject.Find("GamepadManager").GetComponent<GamepadManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Gamepad.current != null)
        {
            if (Gamepad.current.startButton.wasPressedThisFrame)
            {
                gamepadManager.PlayerJoined(Gamepad.current.deviceId);
            }
        }

        for (int i = 0; i < gamepadManager.PlayerCount(); i++)
        {
            if (gamepadManager.PlayerStatus(i) > -1)
            {
                playerText[i].text = "Connected";
            }
            if (gamepadManager.PlayerStatus(i) == -1)
            {
                playerText[i].text = "Disconnected";
            }
        }
    }
}
