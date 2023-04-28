using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    private static float currentSpeed = 5;
    private float rotationDuration = 0.5f, scallingDuration = 0.5f;
    private bool isDancing = false, isSquidGame = false;
    public float rotationSpeed = 360f;
    private DarkBrightMode screen;
    private CameraFollow cameraFollow;
    private NPCController npcController;

    private void Awake()
    {
        screen = FindObjectOfType<DarkBrightMode>();
        cameraFollow = FindObjectOfType<CameraFollow>();
    }


    void Update()
    {
        if (!isDancing)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1))
                PerformRotateDance();

            if (Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Keypad2))
                PerformScallingDance();

            if (Input.GetKeyDown(KeyCode.Alpha3) || Input.GetKeyDown(KeyCode.Keypad3))
                PerformCircleDance();

            if (Input.GetKeyDown(KeyCode.Alpha4) || Input.GetKeyDown(KeyCode.Keypad4))
                PerformJumpingDance();
        }
        MovePlayer();
    }

    void MovePlayer()
    {
        if (isSquidGame && (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A)) )
        {
            isSquidGame = false;
            FindObjectOfType<SceneLoader>().LoadMainScene();
        }

        if (Input.GetKey(KeyCode.W))
            transform.position += new Vector3(0, currentSpeed) * Time.deltaTime;

        if (Input.GetKey(KeyCode.S))
            transform.position += new Vector3(0, -currentSpeed) * Time.deltaTime;

        if (Input.GetKey(KeyCode.D))
            transform.position += new Vector3(currentSpeed, 0) * Time.deltaTime;

        if (Input.GetKey(KeyCode.A))
            transform.position += new Vector3(-currentSpeed, 0) * Time.deltaTime;
    }

    void PerformRotateDance()
    {
        if (isDancing) return;
        StartCoroutine(RotateDance(transform));
    }
    
    void PerformScallingDance()
    {
        if (isDancing) return;
        StartCoroutine(ScallingDance(transform));
    }

    void PerformCircleDance()
    {
        if (isDancing) return;
        StartCoroutine(CircleDance(transform));
    }
    
    void PerformJumpingDance()
    {
        if (isDancing) return;
        StartCoroutine(JumpingDance(transform));
    }

    IEnumerator RotateDance(Transform transform)
    {
        isDancing = true;
        float elapsedTime = 0f;
        while (elapsedTime < rotationDuration)
        {
            float angle = 360f * (Time.deltaTime / rotationDuration);
            transform.Rotate(Vector3.forward, angle);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.rotation = Quaternion.identity;
        isDancing = false;
        
    }

    IEnumerator CircleDance(Transform transform)
    {
        isDancing = true;

        StopDiscoLights();
        StartBlackScreen();

        float timeElapsed = 0f;
        while(timeElapsed < 1f)
        {
            float angle = timeElapsed * rotationSpeed;
            Vector3 newPosition = new Vector3(Mathf.Sin(angle * Mathf.Deg2Rad), Mathf.Cos(angle * Mathf.Deg2Rad), 0f ) * 5f;
            transform.position = newPosition;
            timeElapsed += Time.deltaTime/3;
            PerformRotateDance();
            yield return null;
        }

        StartDiscoLights();
        StopBlackScreen();

        isDancing = false;
    }
    IEnumerator ScallingDance(Transform transform)
    {
        isDancing = true;
        float timeElapsed = 0f;
        Vector3 originalScale = transform.localScale;
        Vector3 targetScale = originalScale * 0.5f;

        while (timeElapsed < scallingDuration)
        {
            transform.localScale = Vector3.Lerp(originalScale, targetScale, timeElapsed / scallingDuration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        transform.localScale = targetScale;

        timeElapsed = 0;
        while (timeElapsed < scallingDuration)
        {
            transform.localScale = Vector3.Lerp(targetScale, originalScale, timeElapsed / scallingDuration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        transform.localScale = originalScale;
        isDancing = false;
    }

    IEnumerator JumpingDance(Transform transform)
    {
        isDancing = true;

        StopCameraFollow();
        StopDiscoLights();
        StartBlackScreen();

        float elapsedTime = 0f, rotationDuration = 1f, jumpDuration = 1f, jumpHeight = 20.0f;
        Vector3 startPos = transform.position;
        
        // Rotate fast for 1 Seconds
        while (elapsedTime < rotationDuration)
        {
            float angle = 360f * (Time.deltaTime / 0.1f);
            transform.Rotate(Vector3.forward, angle);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.rotation = Quaternion.identity;

        elapsedTime = 0f;

        // Jump Up for 1 Second
        while (elapsedTime < jumpDuration)
        {
            float t = elapsedTime / jumpDuration;
            transform.position = startPos + new Vector3(0, Mathf.Sin(t * Mathf.PI) * jumpHeight, 0);

            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.position = startPos;

        
        // Repeat near Jumping
        for (int i = 0; i < 3; i++)
        {
            elapsedTime = 0f;
            while (elapsedTime < jumpDuration)
            {
                float t = elapsedTime / jumpDuration;
                transform.position = startPos + new Vector3(0, Mathf.Sin(t * Mathf.PI) * (elapsedTime + 1f), 0);

                elapsedTime += 1.5f * Time.deltaTime;
                yield return null;
            }
        }

        transform.position = startPos;

        StartCameraFollow();
        StartDiscoLights();
        StopBlackScreen();

        isDancing = false;
    }

    public static void ChangeSpeed(float speed)
    {
        currentSpeed = speed;
    }
    public static void IncreaseScale(Transform transform)
    {
        transform.localScale = transform.localScale * 1.02f;
    }
    public static void DecreaseScale(Transform transform)
    {
        transform.localScale = transform.localScale / 1.02f;
    }

    void StopDiscoLights()
    { 
        DiscoLight[] discoLights= FindObjectsOfType<DiscoLight>();
        foreach (DiscoLight discoLight in discoLights)
            discoLight.StopChangeColor();
    }

    void StartDiscoLights()
    {
        DiscoLight[] discoLights = FindObjectsOfType<DiscoLight>();
        foreach (DiscoLight discoLight in discoLights)
            discoLight.StartChangeColor();
    }

    void StartBlackScreen()
    {
        screen.SetScreenBrightness(0);
    }

    void StopBlackScreen()
    {
        screen.SetScreenBrightness(1);
    }

    void StartCameraFollow()
    {
        cameraFollow.StartFollow();
    }

    void StopCameraFollow()
    {
        cameraFollow.StopFollow();
    }

    public void SetIsSquidGame(bool value)
    {
        isSquidGame = value;
    }

}
