using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyBullet : MonoBehaviour{

    [SerializeField] private GameObject particle;
    [SerializeField] private WeaponDamageEnemy Weapon;

    private PlayerStats Player;
    private Vector2 targetPos;

    [SerializeField] private float speed;
    [SerializeField] private int damage;
    [SerializeField] private int knockback;

    private void Awake(){

        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
    }

    private void Start(){

        targetPos = Player.transform.position;

        //setting attack value to weapondamage script base on attack class
        Weapon.SetAttack(damage, knockback, 0f);
    }

    private void Update(){

        if (Vector2.Distance(transform.position, targetPos) > 0.1f){

            transform.position = Vector2.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
        }
        else{

            Destroy(gameObject, 1f);
            //Instantiate(particle, transform.position, transform.rotation);
        }
    }
}
