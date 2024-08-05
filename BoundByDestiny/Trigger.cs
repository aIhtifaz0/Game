using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{   
    public bool Pass;
    public GameObject UIBoss;

    private void OnTriggerEnter2D(Collider2D collider){

        if(collider.gameObject.tag == "Player"){

            if(!Pass){

                Pass = true;
                UIBoss.SetActive(true);
            }
        }
    }
}
