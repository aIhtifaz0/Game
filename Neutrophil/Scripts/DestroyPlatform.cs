using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyPlatform : MonoBehaviour
{
    [SerializeField] private GameObject Effect;
    [SerializeField] private GameObject Object;

    [SerializeField] private GameObject Enemies;

    private void OnTriggerEnter2D(Collider2D collision){

        if(collision.gameObject.tag == "Player"){

            Instantiate(Effect, transform.position, Quaternion.identity);
            Destroy(Object, 1.5f);
            Enemies.SetActive(true);
        }
    }
}
