using UnityEngine;
using System;
using System.Collections.Generic;
using System.Collections;


public class PlayerStats : MonoBehaviour{

    [SerializeField] private PlayerStateMachine stateMachine;
    [SerializeField] private AstralStateMachine astralStateMachine;

    [SerializeField] public int maxHealth;
    [SerializeField] public int health;
    private bool isInvulnerable;

    private float timer;

    //return true if health equal zero 
    public bool isDie => health <= 0;

    //event for take damage
    public event Action OnTakeDamage;
    public event Action OnDie;
    
    private void Start(){ 

        health = maxHealth;
    }

    private void Update() {

    }

    public void SetInvulnerable(bool isInvulnerable){

        this.isInvulnerable = isInvulnerable;
    }

    public void DealDamage(int damage){
        
        if(health <= 0) { return; }

        if(isInvulnerable) { return; }
        
        //assign health after get damage or 0 when pass below than 0
        health = Mathf.Max(health - damage, 0);

        //call the event
        OnTakeDamage?.Invoke();

        if(astralStateMachine != null){

            //astralStateMachine.SlowMo(0.15f);
            Instantiate(astralStateMachine.HitVfx, astralStateMachine.transform.position, astralStateMachine.HitVfx.transform.rotation);
        }

        if(stateMachine != null){

            //stateMachine.SlowMo(0.15f);
            Instantiate(stateMachine.HitVfx, stateMachine.transform.position, stateMachine.HitVfx.transform.rotation);
        }
        
        //after take damage check health again
        if(health <= 0){
            OnDie?.Invoke();
        }
    }

     public IEnumerator DamageOnStay(int damage){

        for(int currentHealth = health; currentHealth >= 0; currentHealth -= damage){

            stateMachine.SlowMo(0.15f);
            OnTakeDamage?.Invoke();

            if(stateMachine.Astral.IsAstral){

                astralStateMachine.SlowMo(0.15f);
                Instantiate(astralStateMachine.HitVfx, astralStateMachine.transform.position, astralStateMachine.HitVfx.transform.rotation);
            }
            else{

                stateMachine.SlowMo(0.15f);
                Instantiate(stateMachine.HitVfx, stateMachine.transform.position, stateMachine.HitVfx.transform.rotation);
            }


            health = currentHealth;
            yield return new WaitForSeconds(2f);
        }
        health = 0;

        //after take damage check health again
        if(health <= 0){
            OnDie?.Invoke();
        }
    }
}
