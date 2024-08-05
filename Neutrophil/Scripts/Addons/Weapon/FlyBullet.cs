using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class FlyBullet : MonoBehaviour{

    [SerializeField] private GameObject particle;
    [SerializeField] private WeaponDamageEnemy Weapon;

    private PlayerStats Player;
    private Vector2 targetPos;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float speed;
    [SerializeField] private float force;
    [SerializeField] private int damage;
    [SerializeField] private int knockback;

    private void Awake(){

        rb = GetComponent<Rigidbody2D>();
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
    }

    private void Start(){

        targetPos = Player.transform.position;
        rb.gravityScale = 3f;
        rb.AddForce(Vector2.up * force, ForceMode2D.Impulse);

        //setting attack value to weapondamage script base on attack class
        Weapon.SetAttack(damage, knockback, 0f);
    }

    private void Update(){

        if (Vector2.Distance(transform.position, targetPos) > 0.1f){
            transform.position = Vector2.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
        }
        else{
            Instantiate(particle, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {

        if(other.CompareTag("Ground")){
            if(particle != null)
                Instantiate(particle, transform.position, Quaternion.identity);
            
            Destroy(gameObject);
        }    
        if(other.CompareTag("Player")){
            if(particle != null)
                Instantiate(particle, transform.position, Quaternion.identity);
            
            Destroy(gameObject);
        }    
    }
}
