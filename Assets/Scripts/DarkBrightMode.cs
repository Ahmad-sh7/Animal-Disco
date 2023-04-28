using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DarkBrightMode : MonoBehaviour
{
    private float minBrightness = 0.0f, maxBrightness = 1.0f, brightnessStep = 0.1f, updateDelay = 0.05f, currentBrightness = 1f;
    private Image backgroundImage;
    private Coroutine updateCoroutine;

    private void Start()
    {
        backgroundImage = GetComponentInChildren<Image>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.O) && currentBrightness > minBrightness)
        {
            updateCoroutine = StartCoroutine(UpdateBrightness(-brightnessStep));
            StopCoroutine(updateCoroutine);
        }
        else if (Input.GetKeyDown(KeyCode.P) && currentBrightness < maxBrightness)
        {
            updateCoroutine = StartCoroutine(UpdateBrightness(brightnessStep));
            StopCoroutine(updateCoroutine);
        }
    }

    private IEnumerator UpdateBrightness(float step)
    {
        currentBrightness += step;
        UpdateBackgroundImageBrightness();
        yield return new WaitForSeconds(updateDelay); 
    }

    private void UpdateBackgroundImageBrightness()
    {
        Color color = backgroundImage.color;
        color.r = currentBrightness;
        color.g = currentBrightness;
        color.b = currentBrightness;
        backgroundImage.color = color;
    }

    public void SetScreenBrightness(int brightness)
    {
        Color color = backgroundImage.color;
        color.r = brightness;
        color.g = brightness;
        color.b = brightness;
        backgroundImage.color = color;
    }
}
