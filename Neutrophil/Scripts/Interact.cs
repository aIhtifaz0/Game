using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : MonoBehaviour{

    [SerializeField] private GameObject InteractInfo;
    [SerializeField] private Animator anim;

    private bool alreadyPress;

    private void Awake(){
    }

    private void OnTriggerStay2D(Collider2D collision){

        if(collision.gameObject.tag == "Player"){

            if(!alreadyPress){

                InteractInfo.SetActive(true);

                if(Input.GetKeyDown(KeyCode.F)){

                    anim.SetTrigger("View");

                    alreadyPress = true;
                    InteractInfo.SetActive(false);
                }
            }
            else{
                InteractInfo.SetActive(false);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision){

        if(collision.gameObject.tag == "Player"){

            anim.SetTrigger("Back");
            alreadyPress = false;
            InteractInfo.SetActive(false);
        }
    }
}
