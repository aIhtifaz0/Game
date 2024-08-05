using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowEnemy : MonoBehaviour
{
    private Transform enemyTransform;

    private void Awake() {
        
        enemyTransform = GameObject.FindGameObjectWithTag("Enemy").transform;
    }

    private void Update() {

        transform.position = enemyTransform.position;
    }
}
