using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashFX : MonoBehaviour
{
    public GameObject FX;
    public Transform playerPos;

    // Update is called once per frame
    void Update()
    {
        
        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            
            Instantiate(FX, playerPos.position, playerPos.rotation);
           
        }
    }
}
