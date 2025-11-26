// Start the game once the minimum number of players has joined
using Normal.Realtime;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LobbyManager : MonoBehaviour
{
    public RealtimeAvatarManager avatarManager;
    public int minPlayersToStart = 2;
    int playersInLobby = 0;
    [SerializeField] string levelName;

    void Awake()
    {
        avatarManager = FindFirstObjectByType<RealtimeAvatarManager>();
    }

    void Update()
    {
        playersInLobby = avatarManager.avatars.Count;
        PlayerReadyToStart();
    }

    public void PlayerReadyToStart()  
    {
        if (playersInLobby >= minPlayersToStart)
        {
            GameSession.CurrentRoomName = "Room" + GameSession.RoomCounter;
            GameSession.RoomCounter++;
            SceneManager.LoadScene(levelName);
        }
    }
}
