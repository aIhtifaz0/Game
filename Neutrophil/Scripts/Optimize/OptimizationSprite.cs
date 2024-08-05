    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptimizationSprite : MonoBehaviour
{
    
    private SpriteRenderer Sprite;
    // Start is called before the first frame update
    void Start()
    {   
        Sprite = gameObject.GetComponent<SpriteRenderer>();
        Sprite.enabled = false;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "CameraCol")
        {
            Sprite.enabled = true;
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "CameraCol")
        {
            Sprite.enabled = false;
        }
    }
}
