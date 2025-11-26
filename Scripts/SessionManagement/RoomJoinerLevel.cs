// Connects the player to the room specified in GameSession.CurrentRoomName, 
// used for joining different rooms if one is full

using UnityEngine;
using Normal.Utility;
using Normal.Realtime;

public class RoomJoinerLevel : MonoBehaviour
{
    RoomConnector roomConnector;

    private void Awake()
    {
        roomConnector = GetComponent<RoomConnector>();
        if (roomConnector != null)
        {
            roomConnector.ConnectToRoom(GameSession.CurrentRoomName); 
        } 
    }
}
