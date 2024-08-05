using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayShow : MonoBehaviour
{
    public GameObject obj;
    public float lifetime = 55f;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("show", lifetime);
    }

    // Update is called once per frame
    void show()
    {
        obj.SetActive(true);
    }
}
