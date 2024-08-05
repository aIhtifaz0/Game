using System.Collections;
using UnityEngine;

public class UIAnimation : MonoBehaviour
{
    public GameObject[] toFadeIn;
    public float fadeInDuration = 1f; // Duration of the fade-in animation in seconds

    void Start()
    {
        //Add itself to the list of objects to fade in and all its children
        toFadeIn = new GameObject[transform.childCount + 1];
        toFadeIn[0] = gameObject;
        for (int i = 0; i < transform.childCount; i++)
        {
            toFadeIn[i + 1] = transform.GetChild(i).gameObject;
        }
        // Start the fade-in animation for each GameObject
        foreach (GameObject obj in toFadeIn)
        {
            StartCoroutine(FadeIn(obj, fadeInDuration));
        }
    }

    IEnumerator FadeIn(GameObject obj, float duration)
    {
        CanvasGroup canvasGroup = obj.GetComponent<CanvasGroup>();
        if (canvasGroup != null)
        {
            // Initialize alpha to 0 (completely transparent)
            canvasGroup.alpha = 0;
            float elapsedTime = 0;

            // Gradually increase alpha from 0 to 1
            while (elapsedTime < duration)
            {
                canvasGroup.alpha = elapsedTime / duration;
                elapsedTime += Time.deltaTime;
                yield return null; // Wait for the next frame
            }

            // Ensure the alpha is set to 1 (completely opaque)
            canvasGroup.alpha = 1;
        }
    }
}