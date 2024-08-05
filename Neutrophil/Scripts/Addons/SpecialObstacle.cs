using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialObstacle : MonoBehaviour{

    [SerializeField] private WeaponDamageEnemy Weapon;

    [SerializeField] private float speed;
    [SerializeField] private int damage;
    [SerializeField] private int knockback;


    private void Start(){

        //setting attack value to weapondamage script base on attack class
        Weapon.SetAttack(damage, knockback, 0f);
    }

    private void Update(){

        transform.Translate(Vector2.right * speed * Time.deltaTime);

        Object.Destroy(gameObject, 15);
    }
}
