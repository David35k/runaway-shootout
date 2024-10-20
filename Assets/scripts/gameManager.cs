using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameManager : MonoBehaviour
{
    public GameObject[] cameras;
    public GameObject[] players;
    public GameObject[] coolTriggers;
    public GameObject escape;
    public GameObject winnerText;
    void Awake()
    {
#if UNITY_EDITOR
        QualitySettings.vSyncCount = 0;  // VSync must be disabled
        Application.targetFrameRate = 60;
#endif

        for (int i = 0; i < players.Length; i++)
        {
            players[i].GetComponent<Rigidbody>().MovePosition(players[i].GetComponent<playa>().spawnPoint.transform.position);
            players[i].GetComponent<Rigidbody>().MoveRotation(players[i].GetComponent<playa>().spawnPoint.transform.rotation);
        }

        winnerText.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        // winna winna chicken dinna
        if (escape.GetComponent<winTrigger>().won)
        {
            Time.timeScale = 0.05f;
            winnerText.SetActive(true);
        }

        for (int i = 0; i < coolTriggers.Length; i++)
        {
            // triger the coolness!!!!!!!!!!!!!!!!!!!!!! 😎😎😎😎
            if (coolTriggers[i].GetComponent<endTrigger>().escaping)
            {
                Time.timeScale = 0.3f;
                return;
            }

            Time.timeScale = 1f;

        }
    }

    public void shakeEm(float shakeDuration, float shakeMagnitude, int playerNumber)
    {
        cameras[playerNumber - 1].GetComponent<CameraController>().StartShake(shakeDuration, shakeMagnitude);
    }
}
