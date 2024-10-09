using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameManager : MonoBehaviour
{
    public GameObject[] cameras;
    void Awake()
    {
#if UNITY_EDITOR
        QualitySettings.vSyncCount = 0;  // VSync must be disabled
        Application.targetFrameRate = 60;
#endif
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void shakeEm(float shakeDuration, float shakeMagnitude, int playerNumber)
    {
        cameras[playerNumber - 1].GetComponent<CameraController>().StartShake(shakeDuration, shakeMagnitude);
    }
}
