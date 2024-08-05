using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour{

    [SerializeField] private GameObject InteractInfo;
    [SerializeField] private GameObject LeverInfo;

    private bool alreadyPress;

    private void OnTriggerStay2D(Collider2D collision){

        if(collision.gameObject.tag == "Player"){

            if(!alreadyPress){

                InteractInfo.SetActive(true);

                if(Input.GetKeyDown(KeyCode.F)){

                    alreadyPress = true;
                    InteractInfo.SetActive(false);
                    LeverInfo.SetActive(true);
                }
            }
            else{
                InteractInfo.SetActive(false);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision){

        if(collision.gameObject.tag == "Player"){

            alreadyPress = false;
            LeverInfo.SetActive(false);
            InteractInfo.SetActive(false);
        }
    }
}
