using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class disablePlatform : MonoBehaviour
{
    BoxCollider2D bc;
    [SerializeField]
    GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        bc = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {

        if((Input.GetKey(KeyCode.S) && Input.GetKeyDown(KeyCode.Space)) || (Input.GetKeyDown(KeyCode.S) && Input.GetKey(KeyCode.Space)))
        {
             bc.enabled = false;
             Invoke("anu", 0.3f);
        }
        
        
    }

    void anu()
    {
        bc.enabled=true;
    }
}
