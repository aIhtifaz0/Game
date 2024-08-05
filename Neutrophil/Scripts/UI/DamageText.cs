using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamageText : MonoBehaviour
{

    public void SetText(string text)
    {
        GetComponent<TMP_Text>().text = text;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator FadeAndMoveUp(float duration)
    {
        float elapsedTime = 0;
        Vector3 startPosition = transform.localPosition;
        Vector3 endPosition = startPosition + new Vector3(0, 100, 0); // Adjust this value as needed
        Color startColor = GetComponent<TMP_Text>().color;
        Color endColor = new Color(startColor.r, startColor.g, startColor.b, 0);

        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;
            transform.localPosition = Vector3.Lerp(startPosition, endPosition, t);
            GetComponent<TMP_Text>().color = Color.Lerp(startColor, endColor, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        Destroy(gameObject);
    }
}
