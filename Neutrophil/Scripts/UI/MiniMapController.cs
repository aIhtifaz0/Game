using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MiniMapController : MonoBehaviour, IDragHandler, IPointerEnterHandler, IPointerExitHandler
{
    public RawImage rawImage;
    private bool isHovering = false;
    public float sensitivity = 0.01f; // Adjust this value as needed
    public Vector2 xLimit = new Vector2(-0.5f, 0.5f); // Adjust these values as needed
    public Vector2 yLimit = new Vector2(-0.5f, 0.5f); // Adjust these values as needed

    public void OnDrag(PointerEventData eventData)
    {
        if (isHovering)
        {
            float newX = Mathf.Clamp(rawImage.uvRect.x - eventData.delta.x * sensitivity, xLimit.x, xLimit.y);
            float newY = Mathf.Clamp(rawImage.uvRect.y - eventData.delta.y * sensitivity, yLimit.x, yLimit.y);
            rawImage.uvRect = new Rect(newX, newY, rawImage.uvRect.width, rawImage.uvRect.height);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isHovering = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isHovering = false;
    }

}