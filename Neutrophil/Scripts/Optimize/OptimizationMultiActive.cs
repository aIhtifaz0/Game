using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptimizationMultiActive : MonoBehaviour
{
    public GameObject obj;
    public GameObject obj2;
    public GameObject obj3;
    public GameObject obj4;
    // Start is called before the first frame update
    void Start()
    {
        obj.SetActive(false);
        obj2.SetActive(false);
        obj3.SetActive(false);
        obj4.SetActive(false);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "CameraCol")
        {
            obj.SetActive(true);
            obj2.SetActive(true);
            obj3.SetActive(true);
            obj4.SetActive(true);
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "CameraCol")
        {
            obj.SetActive(false);
            obj2.SetActive(false);
            obj3.SetActive(false);
            obj4.SetActive(false);
        }
    }
}
