using System.Collections.Generic;
using UnityEngine;

public class WeaponDamagePlayer : MonoBehaviour{

    [SerializeField] private Collider2D myCollider;
    [SerializeField] private PlayerStats stats;

    private int damage;
    private float knockback;
    private float upKnockback;
    private float downKnockback;
    private float backKnockback;

    [SerializeField] private Rigidbody2D Rigidbody;

    private List<Collider2D> alreadyCollidedWith = new List<Collider2D>();

    private void OnTriggerEnter2D (Collider2D other){

        //checking if hitting player collider it self
        if(other == myCollider){ return; }

        //checking if already hit other once when the script active then return
        //if not added other to the list for another checking 
        if(alreadyCollidedWith.Contains(other)){ return; }
        
        alreadyCollidedWith.Add(other);

        if (other.TryGetComponent(out EnemyStats enemyHealth)) {
            enemyHealth.DealDamage(damage);
            alreadyCollidedWith.Clear();
        }

        if (other.TryGetComponent(out MeleeForceReceiver meleeForceReceiver)){

            meleeForceReceiver.AddForce(other.transform, knockback, upKnockback, downKnockback, backKnockback);
        }

        if (other.TryGetComponent(out FlyForceReceiver flyForceReceiver)){

            flyForceReceiver.AddForce(other.transform, knockback, upKnockback, downKnockback, backKnockback);
        }

        if (other.TryGetComponent(out RangeForceReceiver rangeForceReceiver)){

            rangeForceReceiver.AddForce(other.transform, knockback, upKnockback, downKnockback, backKnockback);
        }

        if (other.TryGetComponent(out EliteMeleeForceReceiver eliteMeleeForceReceiver)){

            eliteMeleeForceReceiver.AddForce(other.transform, knockback, upKnockback, downKnockback, backKnockback);
        }
    }

    public void SetAttack(int damage, float knockback, float upKnockback, float downKnockback, float backKnockback){

        //getting damage value from attacking state then applied at dealdamage from setattack
        this.damage = damage;
        this.knockback = knockback;
        this.upKnockback = upKnockback;
        this.downKnockback = downKnockback;
        this.backKnockback = backKnockback;
    }
}
