using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowElite : MonoBehaviour
{
    [SerializeField]
    private Transform enemyTransform;

    private void Update() {

        if(enemyTransform != null){

            transform.position = enemyTransform.position;
        }
        else{

            Destroy(gameObject);        
        }
    }
}
