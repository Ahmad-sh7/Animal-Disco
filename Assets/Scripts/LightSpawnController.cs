using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSpawnController : MonoBehaviour
{
    [SerializeField] GameObject lightPrefab;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
            SpawnLight();
    }

    void SpawnLight()
    {
        Camera mainCamera = Camera.main;
        Vector3 cameraPosition = mainCamera.transform.position;
        float cameraHeight = 2f * mainCamera.orthographicSize;
        float cameraWidth = cameraHeight * mainCamera.aspect;

        // Calculate the bounds in which the light can spawn
        float xMin = cameraPosition.x - cameraWidth / 2f;
        float xMax = cameraPosition.x + cameraWidth / 2f;
        float yMin = cameraPosition.y - cameraHeight / 2f;
        float yMax = cameraPosition.y + cameraHeight / 2f;

        Vector3 randomPosition = new Vector3(Random.Range(xMin, xMax), Random.Range(yMin, yMax), 0f);
        Instantiate(lightPrefab, randomPosition, Quaternion.identity);
    }
}
