using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOneWay : MonoBehaviour {

    private PlatformEffector2D effector;
    private float waitTime = 0.3f;

    private void Awake(){

        effector = GetComponent<PlatformEffector2D>();
    } 

    private void Update(){

        if(Input.GetKeyUp(KeyCode.S)){

            waitTime = 0.3f;
        }

        if(Input.GetKey(KeyCode.S)){

            if(waitTime <= 0f){
                effector.rotationalOffset = 180f;
                waitTime = 0.3f;    
            }
        }
        else{
            waitTime -= Time.deltaTime;
            effector.rotationalOffset = 0f;
        }
        
    } 
}
