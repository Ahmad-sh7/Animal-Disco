using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    private static float currentSpeed = 5;
    private float rotationDuration = 0.5f, scallingDuration = 0.5f;
    private bool isDancing = false;

    void Update()
    {
        if (!isDancing)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1))
                PerformRotateDance();

            if (Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Keypad2))
                PerformScallingDance();
        }
        MovePlayer();
    }

    void MovePlayer()
    {
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

    public static void ChangeSpeed(float speed)
    {
        currentSpeed = speed;
    }

}
