using System.Collections.Generic;
using UnityEngine;

public class WeaponDamage : MonoBehaviour{

    [SerializeField] private Collider2D myCollider;

    private int damage;
    private float knockback;

    private List<Collider2D> alreadyCollidedWith = new List<Collider2D>();

    private void OnEnable(){

        //clearing list every time the script activated
        alreadyCollidedWith.Clear();
    }

    private void OnTriggerEnter2D (Collider2D other){

        //checking if hitting player collider it self
        if(other == myCollider){ return; }

        //checking if already hit other once when the script active then return
        //if not added other to the list for another checking 
        if(alreadyCollidedWith.Contains(other)){ return; }
        
        alreadyCollidedWith.Add(other);

        //checking if hitting other that have health than deal damage
        if(other.TryGetComponent(out PlayerStats playerHealth)){

            playerHealth.DealDamage(damage);
        }

        if (other.TryGetComponent(out EnemyStats enemyHealth)) {

            enemyHealth.DealDamage(damage);
        }
        
    }

    public void SetAttack(int damage, float knockback){

        //getting damage value from attacking state then applied at dealdamage from setattack
        this.damage = damage;
        this.knockback = knockback;
    }
}
