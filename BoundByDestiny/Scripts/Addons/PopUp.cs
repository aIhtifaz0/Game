using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUp : MonoBehaviour{

    [SerializeField] private GameObject TouchInfo;

    private bool alreadyPress;

    private void OnTriggerStay2D(Collider2D collision){

        if(collision.gameObject.tag == "Player"){

            if(!alreadyPress){

                TouchInfo.SetActive(true);

                if(Input.GetKeyDown(KeyCode.F)){

                    alreadyPress = true;
                    TouchInfo.SetActive(false);
                }
            }
            else{
                TouchInfo.SetActive(false);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision){

        if(collision.gameObject.tag == "Player"){

            alreadyPress = false;
            TouchInfo.SetActive(false);
        }
    }
}
