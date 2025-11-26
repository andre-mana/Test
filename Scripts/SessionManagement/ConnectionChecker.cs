// disconnects the player if there's no connection

using Normal.Realtime;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ConnectionShecker : MonoBehaviour
{
    private void Start()
    {
        realtime = FindFirstObjectByType<Realtime>();
        StartCoroutine(CheckConnection());
        realtime.didDisconnectFromRoom += OnDisconnected;
    }

    private void OnDisconnected(Realtime realtime)
    {
        disconnectedTextObj.SetActive(true);
        StartCoroutine(GoToMainMenu());
    }

    private IEnumerator GoToMainMenu()
    {
        yield return new WaitForSeconds(2f);  
        SceneManager.LoadScene("mainMenu");   
    }

    IEnumerator CheckConnection()
    {
        float timeout = 5f; 
        float timer = 0f;

        while (!realtime.connected && timer < timeout)
        {
            timer += Time.deltaTime;
            yield return null;
        }

        if (!realtime.connected)
        {
            SceneManager.LoadScene("mainMenu");
            GoToMainMenu();
        } 
    }
}

