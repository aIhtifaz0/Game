using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] private WeaponDamageEnemy Weapon;

    private PlayerStats Player;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private GameObject particle;
    [SerializeField] private float speed;
    [SerializeField] private float force;
    [SerializeField] private int damage;
    [SerializeField] private int knockback;

    private void Awake(){

        rb = GetComponent<Rigidbody2D>();
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
    }

    private void Start(){

        //setting attack value to weapondamage script base on attack class
        Weapon.SetAttack(damage, knockback, 0f);
    }

    private void Update(){

        Vector2 lookDir = Player.transform.position - transform.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        Quaternion rotate = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = rotate;
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
