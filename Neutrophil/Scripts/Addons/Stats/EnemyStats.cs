using UnityEngine;
using System;

public class EnemyStats : MonoBehaviour {

    [field:SerializeField] public MeleeStateMachine stateMachine { get; private set; }

    public int maxHealth;
    
    [HideInInspector]
    public int health;
    private bool isInvulnerable;

    //return true if health equal zero 
    public bool isDie => health == 0;

    //event for take damage
    public event Action OnTakeDamage;
    public event Action OnDie;

     public void SetInvulnerable(bool isInvulnerable) {

         this.isInvulnerable = isInvulnerable;
    }

    private void Start() {

        health = maxHealth;
    }

    public void DealDamage(int damage) {

        if (health == 0) { return; }

        if (isInvulnerable) { return; }

        //assign health after get damage or 0 when pass below than 0
        health = Mathf.Max(health - damage, 0);

        //call the event
        OnTakeDamage?.Invoke();

        //after take damage check health again
        if (health <= 0) {
            OnDie?.Invoke();
        }
    }
}
