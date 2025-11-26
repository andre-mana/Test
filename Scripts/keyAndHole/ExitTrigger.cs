using Normal.Realtime;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitTrigger : MonoBehaviour
{  
    private void OnTriggerEnter(Collider other)
    { 
        var playerView = other.transform.root.GetComponent<RealtimeView>();
        if (!playerView.isOwnedLocallyInHierarchy) return;

        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene("mainMenu");
        }
    } 
}

