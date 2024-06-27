using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyLevelScreen : MonoBehaviour
{
    public GameObject loadScreen;
    public float fadeDuration = 2f;

    private void Start()
    {
        StartCoroutine(FadeOutAndDestroy(loadScreen, fadeDuration));
    }

    IEnumerator FadeOutAndDestroy(GameObject obj, float duration)
    {
        CanvasGroup canvasGroup = obj.GetComponent<CanvasGroup>();

        if (canvasGroup != null)
        {
            for (float t = 0; t < duration; t += Time.deltaTime)
            {
                canvasGroup.alpha = 1 - t / duration;
                yield return null;
            }
        }

        Destroy(obj);
    }
}
