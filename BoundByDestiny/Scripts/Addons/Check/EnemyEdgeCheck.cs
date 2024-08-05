using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyEdgeCheck : MonoBehaviour{

    public event Action ChangeDirection;

    private void OnTriggerEnter2D(Collider2D other) {

        if(other.gameObject.CompareTag("Edge")){

            ChangeDirection?.Invoke();
        }
    }
}
