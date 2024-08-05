using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageArea : MonoBehaviour{

    [SerializeField] private int damage;
    [SerializeField] private float knockback;

    [SerializeField] private Transform myTransform;

    private Coroutine DamageStay;


    private List<Collider2D> alreadyCollidedWith = new List<Collider2D>();

    private void OnEnable(){

        //clearing list every time the script activated
        alreadyCollidedWith.Clear();
    }


    private void OnTriggerEnter2D (Collider2D other){


        //checking if hitting other that have health than deal damage
        if(other.TryGetComponent(out PlayerStats playerHealth)){

            DamageStay = StartCoroutine(playerHealth.DamageOnStay(damage));
        }

        if (other.TryGetComponent(out PlayerStateMachine playerStateMachine)){

            playerStateMachine.ForceReceiver.AddForce(myTransform, knockback, 0f);
        }

        if (other.TryGetComponent(out AstralStateMachine astralStateMachine)){

            astralStateMachine.ForceReceiver.AddForce(myTransform, knockback, 0f);
        }
    }

    private void OnTriggerExit2D (Collider2D other){

        //checking if hitting other that have health than deal damage
        if(other.TryGetComponent(out PlayerStats playerHealth)){

            StopCoroutine(DamageStay);
        }
    }


}
