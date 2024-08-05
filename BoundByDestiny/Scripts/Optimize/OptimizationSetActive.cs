using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptimizationSetActive : MonoBehaviour
{
    public GameObject obj;
    // Start is called before the first frame update
    void Start()
    {
        obj.SetActive(false);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "CameraCol")
        {
            obj.SetActive(true);
            
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "CameraCol")
        {
            obj.SetActive(false);

        }
    }
}
