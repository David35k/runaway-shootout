using System.Collections;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    public float cameraSpeed = 2.8f;

    private bool isShaking = false;
    // private Vector3 originalPosition;

    void Start()
    {
        // if (player != null)
        // {
        //     originalPosition = transform.position;
        // }
    }

    void Update()
    {
        if (player != null && !isShaking)
        {
            // Camera follows the player
            transform.position = Vector3.Lerp(transform.position, new Vector3(player.transform.position.x, player.transform.position.y + 2.5f, transform.position.z), Time.deltaTime * cameraSpeed);
        }
    }

    public void StartShake(float shakeDuration, float shakeMagnitude)
    {
        if (!isShaking)
        {
            StartCoroutine(ShakeCamera(shakeDuration, shakeMagnitude));
        }
    }

    IEnumerator ShakeCamera(float shakeDuration, float shakeMagnitude)
    {
        isShaking = true;
        float elapsedTime = 0f;

        while (elapsedTime < shakeDuration)
        {
            Vector3 randomOffset = Random.insideUnitSphere * shakeMagnitude;
            randomOffset.z = 0; // Keep the z-axis unaffected

            transform.position = Vector3.Lerp(transform.position, new Vector3(player.transform.position.x, player.transform.position.y + 2.5f, transform.position.z), Time.deltaTime * cameraSpeed) + randomOffset;
            elapsedTime += Time.deltaTime;

            yield return null;
        }

        // Reset camera position
        // transform.position = originalPosition;
        isShaking = false;
    }
}
