using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeAttackState : RangeBaseState{

    private RangeAttack attack;
    private float previousFrameTime;
    private bool alReadyAppliedForce;
    public RangeAttackState(RangeStateMachine stateMachine, int AttackIndex) : base(stateMachine){

        attack = stateMachine.Attack;
    }

    public override void Enter(){

        stateMachine.Audio.PlaySound(stateMachine.Audio.Attack);

        //setting attack value to weapondamage script base on attack class
        stateMachine.Weapon.SetAttack(attack.AttackDamage, attack.AttackKnockback, 0);

        //playing some animation with transition
        stateMachine.Animator.CrossFadeInFixedTime(attack.AnimationName, attack.TransitionDuration);
    }

    public override void Tick(float deltatime){

        CheckPlayerDashing();

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

            if(stateMachine.StrongBullet <= 0 && stateMachine.RangeType == RangeStateMachine.Range.EliteRange){
                stateMachine.SwitchState(new RangeReloadState(stateMachine));
                return;
            }

            if(!IsInAttackRange()){
                stateMachine.SwitchState(new RangeMainState(stateMachine));
            }
        }
        previousFrameTime = normalizedTime;
    }

    public override void Exit(){

        stateMachine.IsAttacking = false;

        if(stateMachine.RangeType == RangeStateMachine.Range.EliteRange)
            stateMachine.IsChargeAttacking = false;
    }

    private void TryComboAttack(float normalizedTime) {

        if (attack.ComboStateIndex == -1) { return; }

        if (normalizedTime < attack.ComboAttackTime) { return; }

        stateMachine.SwitchState(new RangeAttackState(stateMachine, attack.ComboStateIndex));
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
