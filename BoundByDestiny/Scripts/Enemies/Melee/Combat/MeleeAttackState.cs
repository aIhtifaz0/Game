using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttackState : MeleeBaseState{

    private MeleeAttack attack;
    private float previousFrameTime;
     private float duration = 1f;
    private bool alReadyAppliedForce;

    public MeleeAttackState(MeleeStateMachine stateMachine, int AttackIndex) : base(stateMachine) {

        attack = stateMachine.Attacks[AttackIndex];
    }

    public override void Enter() {

        stateMachine.Audio.PlaySound(stateMachine.Audio.Attack);
        stateMachine.Rigidbody.velocity = Vector3.zero;
        //setting attack value to weapondamage script base on attack class
        stateMachine.Weapon.SetAttack(attack.AttackDamage, attack.AttackKnockback, attack.AttackUpKnockback);

        stateMachine.Stats.SetInvulnerable(true);
        //playing some animation with transition
        stateMachine.Animator.CrossFadeInFixedTime(attack.AnimationName, attack.TransitionDuration);
    }

    public override void Tick(float deltaTime) {

        duration -= Time.deltaTime;

        float normalizedTime = GetNormalizedTime(stateMachine.Animator, "Attack");

        if (normalizedTime < 1f) {

            //adding force to forward when attack
            if (normalizedTime >= attack.ForceTime) {

                TryApplyForce();
            }
            
            if (normalizedTime > 0.9f) {
                TryComboAttack(normalizedTime);
            }
        }
        else {
            
            if(duration <= 0f){

                if (!IsInAttackRange()) {

                    if (stateMachine.MeleeType == MeleeStateMachine.Melee.NormalMelee) {
                        stateMachine.SwitchState(new MeleeSpotState(stateMachine));
                        return;
                    }
                    if(stateMachine.MeleeType == MeleeStateMachine.Melee.StrongMelee){
                        stateMachine.IsRecharging = true;
                        stateMachine.SwitchState(new MeleeRechargeState(stateMachine));
                    }
                }
                     if(stateMachine.MeleeType == MeleeStateMachine.Melee.BossMelee){
                        stateMachine.IsRecharging = true;
                        stateMachine.SwitchState(new MeleeRechargeState(stateMachine));
                    }
            }
        }
        previousFrameTime = normalizedTime;
    }

    public override void Exit() {
        stateMachine.Stats.SetInvulnerable(false);
    }

    private void TryComboAttack(float normalizedTime) {

        if (attack.ComboStateIndex == -1) { return; }

        if (normalizedTime < attack.ComboAttackTime) { return; }

        stateMachine.SwitchState(new MeleeAttackState(stateMachine, attack.ComboStateIndex));
        /*
            checking if there is any attack left to combo
            is it done animating at this state
            then change state to new attack state
        */
    }

    private void TryApplyForce() {

        if (alReadyAppliedForce) { return; }

        Vector3 Dir = stateMachine.IsFacingRight ? Vector3.right : Vector3.left; 

        stateMachine.Rigidbody.AddForce(Dir * attack.Force, ForceMode2D.Force);
        alReadyAppliedForce = true;
    }
}
