using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DanceMoveController : MonoBehaviour
{
    public static IEnumerator RotateDance(Transform transform)
    {
        float elapsedTime = 0f;
        while (elapsedTime < 0.5f)
        {
            float angle = 360f * (Time.deltaTime / 0.5f);
            transform.Rotate(Vector3.forward, angle);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.rotation = Quaternion.identity;
    }

    public static IEnumerator ScallingDance(Transform transform)
    {
        Vector3 originalScale = transform.localScale;
        Vector3 targetScale = originalScale * 0.5f;

        float timeElapsed = 0f;
        while (timeElapsed < 0.25f)
        {
            transform.localScale = Vector3.Lerp(originalScale, targetScale, timeElapsed / 0.25f);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        transform.localScale = targetScale;

        timeElapsed = 0f;
        while (timeElapsed < 0.25f)
        {
            transform.localScale = Vector3.Lerp(targetScale, originalScale, timeElapsed / 0.25f);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        transform.localScale = originalScale;
    }
}
