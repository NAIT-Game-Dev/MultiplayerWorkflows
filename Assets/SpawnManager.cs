using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] GameObject spawnPoint;
    [SerializeField] GameObject playerPrefab;
    [SerializeField] List<GameObject> players;
    [SerializeField] List<int> gamepadID;

    // Update is called once per frame
    void Update()
    {
        // If a new gamepad gets plugged in.
        if (Gamepad.all.Count > gamepadID.Count)
        {
            // Spawn a player object for the new gamepad
            GameObject newPlayer = Instantiate(playerPrefab, spawnPoint.transform.position, spawnPoint.transform.rotation);

            // Add the new player object to a list
            players.Add(newPlayer);

            // Set the gamepadID for the player to be the one that just got plugged in
            newPlayer.GetComponent<W1_PlayerMovement>().SetGamepadID(Gamepad.all[players.Count - 1].deviceId);

            // Add the gamepadID to a list
            gamepadID.Add(Gamepad.all[players.Count - 1].deviceId);
            UpdatePlayerID();

        }

        // If all gamepads are disconnected then clear the lists and destroy the last player object
        if (Gamepad.all.Count == 0 && gamepadID.Count > 0)
        {
            gamepadID.Clear();
            Destroy(players[0]);
            players.Clear();                        
        }
        
        // If a gamepad got disconnected then find the player object that was connected to it
        if (Gamepad.all.Count < gamepadID.Count)
        {
            // Go through the list of gamepadIDs
            for (int i = 0; i < gamepadID.Count; i++)
            {
                bool found = false;
                // Search throught the current gamepads that are connected
                for (int j = 0; j < Gamepad.all.Count; j++)
                {
                    // If the gamepadID is in the list of gamepads connected then you found it and do nothing
                    if (gamepadID[i] == Gamepad.all[j].deviceId)
                    {
                        found = true; 
                    }
                }
                // If the gamepadID was not found then remove at that index from both lists and destroy the player object
                if (!found)
                {
                    gamepadID.RemoveAt(i);
                    Destroy(players[i]);
                    players.RemoveAt(i);                    
                }
            }
            // Update the playerIDs so that all gamepads will control the player objects that they did before
            UpdatePlayerID();
        }
    }

    void UpdatePlayerID()
    {
        for (int i = 0; i < gamepadID.Count; i++)
        {
            for (int j = 0; j < players.Count; j++)
            {
                if (players[j].GetComponent<W1_PlayerMovement>().GetGamepadID() == gamepadID[i])
                {
                    players[j].GetComponent<W1_PlayerMovement>().SetPlayerID(i);
                }
            }
        }
    }
}
