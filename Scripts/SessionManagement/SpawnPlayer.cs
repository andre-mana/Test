// Gives each player a unique starting position based on their unique ID, 
// ensuring they don't spawn at the same location

using Normal.GorillaTemplate;
using Normal.Realtime;
using TMPro;
using UnityEngine;

public class SpawnPlayer : MonoBehaviour
{
    [SerializeField] Transform[] spawnPoints;
    [SerializeField] GameObject loadingCanvas;

    RealtimeAvatarManager avatarManager;
    Realtime realtime;
    int index;

    void Awake()
    {
        avatarManager = FindFirstObjectByType<RealtimeAvatarManager>();
        realtime = FindFirstObjectByType<Realtime>();
        realtime.didConnectToRoom += DidConnectToRoom;
        avatarManager.avatarCreated += AvatarCreated;
    }

    private void DidConnectToRoom(Realtime realtime)
    {
        index = realtime.clientID;
        if (index < 0 || index > spawnPoints.Length)
            index = 0;

        transform.position = spawnPoints[index].position;
    }
     
    private void AvatarCreated(RealtimeAvatarManager avatarManager, RealtimeAvatar avatar, bool isLocalAvatar)
    {
        loadingCanvas.SetActive(false);
    }
}
