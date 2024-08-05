using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


[ExecuteInEditMode()]
public class Tooltip : MonoBehaviour
{

    public TMP_Text Header_Text;
    public TMP_Text Content_Text;
    public TMP_Text price_Text;

    public int characterlimit = 80;

    private RectTransform rectTransform;
    // Start is called before the first frame update
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        ToolTipPosision();
    }

    public void SetText(string header, string content, string price)
    {
        Header_Text.text = header;
        Content_Text.text = content;
        price_Text.text = price;
    }

    public void ToolTipPosision()
    {
        Vector2 position = Input.mousePosition;
        

        float pivotX = position.x / Screen.width;
        float pivotY = position.y / Screen.height;

        rectTransform.pivot = new Vector2(pivotX, pivotY);

        transform.position = position;
    }
}
