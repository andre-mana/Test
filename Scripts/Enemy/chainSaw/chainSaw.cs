// When the player collides with the chainsaw, they lose and are sent back to the menu (lobby)

using Normal.Realtime;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class chainSaw : MonoBehaviour
{
    [SerializeField] GameObject DeadUI;
    [SerializeField] Realtime realtime;
    private void OnTriggerEnter(Collider other)
    {
        var playerView = other.transform.root.GetComponent<RealtimeView>();
        if (!playerView.isOwnedLocallyInHierarchy) return;

        if (other.CompareTag("Player"))
        {
            DeadUI.SetActive(true);
            StartCoroutine(ReturnToMenu());
            Destroy(other.transform.root.gameObject);
        }
    }

    IEnumerator ReturnToMenu()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("mainMenu");
    }
}

