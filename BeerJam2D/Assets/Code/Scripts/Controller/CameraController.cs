using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Intensity of the camera shake
    public float shakeIntensity = 0.1f;

    // Duration of the camera shake
    public float shakeDuration = 0.5f;

    // Transform of the camera
    private Transform cameraTransform;

    // Initial position of the camera
    private Vector3 initialPosition;

    public static CameraController _;

    private void OnEnable() => Suscribe(true);
    private void OnDisable() => Suscribe(false);
    private void Suscribe(bool condition)
    {
    }

    private void Awake()
    {
        _ = this;

        cameraTransform = GetComponent<Transform>();
        // Store the initial position of the camera
        initialPosition = cameraTransform.localPosition;
    }

    public void Shake()
    {
        // Debug.Log("shakea");
        StartCoroutine(ShakeCamera());
    }

    private IEnumerator ShakeCamera()
    {
        cameraTransform.localPosition = initialPosition;

        float elapsedTime = 0f;

        while (elapsedTime < shakeDuration)
        {
            // Generate a random offset for the camera position
            Vector3 randomOffset = Random.insideUnitSphere * shakeIntensity;

            // Update the camera position with the random offset
            cameraTransform.localPosition = initialPosition + randomOffset;

            // Increment the elapsed time
            elapsedTime += Time.deltaTime;

            yield return null;
        }

        // Reset the camera position to its initial position
        cameraTransform.localPosition = initialPosition;
    }

}
