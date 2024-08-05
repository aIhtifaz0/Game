using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;
public class PoptiWindow : MonoBehaviour
{

    public float targetWidth;
    public float targetHeight;

    public float lifetime = 5f;
    public string title;
    public Image image;
    public TMP_Text text;
    public float animatorDuration = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        text.text = title;
        StartCoroutine(Open());
    }

    // Update is called once per frame
    void Update()
    {
        lifetime -= Time.deltaTime;
        if (lifetime <= 0)
        {
            StartCoroutine(Close());
        }
    }

    public IEnumerator Open()
    {
        float width = 0;
        float height = targetHeight;
        float time = 0;

        RectTransform rectTransform = GetComponent<RectTransform>(); // Get the RectTransform component

        while (time < animatorDuration)
        {
            time += Time.deltaTime;
            width = Mathf.Lerp(0, targetWidth, time / animatorDuration);
            // Keep the original height constant
            height = rectTransform.sizeDelta.y;

            // Set the size of the RectTransform, changing only the width
            rectTransform.sizeDelta = new Vector2(width, height);

            yield return null;
        }

        rectTransform.sizeDelta = new Vector2(targetWidth, targetHeight);
        image.gameObject.SetActive(true);
        text.gameObject.SetActive(true);

    }

    public IEnumerator Close()
    {
        float width = targetWidth;
        float height = targetHeight;
        float time = 0;

        RectTransform rectTransform = GetComponent<RectTransform>(); // Get the RectTransform component

        // For the Close coroutine
        while (time < animatorDuration)
        {
            time += Time.deltaTime;
            width = Mathf.Lerp(targetWidth, 0, time / animatorDuration);
            // Keep the original height constant
            height = rectTransform.sizeDelta.y;

            // Set the size of the RectTransform, changing only the width
            rectTransform.sizeDelta = new Vector2(width, height);

            yield return null;
        }

        Destroy(gameObject);
    }
}
