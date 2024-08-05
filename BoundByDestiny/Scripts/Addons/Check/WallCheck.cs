using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallCheck : MonoBehaviour{

    [SerializeField] private PlayerStateMachine stateMachine;

    private void OnTriggerEnter2D(Collider2D other){

        if(other.CompareTag("Wall")){

            stateMachine.IsWall = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other){

        if(other.CompareTag("Wall")){

            stateMachine.IsWall = false;
        }
    }
}
