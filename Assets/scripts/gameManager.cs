using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameManager : MonoBehaviour
{
    public GameObject[] cameras;
    public GameObject[] players;
    public GameObject coolTrigger;
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
        // triger the coolness!!!!!!!!!!!!!!!!!!!!!! 😎😎😎😎
        if (coolTrigger.GetComponent<endTrigger>().escaping)
        {
            Time.timeScale = 0.3f;
        }
        else
        {
            Time.timeScale = 1f;
        }

        // winna winna chicken dinna
        if (escape.GetComponent<winTrigger>().won)
        {
            Time.timeScale = 1f;
            winnerText.SetActive(true);
        }
    }

    public void shakeEm(float shakeDuration, float shakeMagnitude, int playerNumber)
    {
        cameras[playerNumber - 1].GetComponent<CameraController>().StartShake(shakeDuration, shakeMagnitude);
    }
}
