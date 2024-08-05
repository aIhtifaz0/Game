using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EliteMeleeAttackState : EliteMeleeBaseState{

    private EliteMeleeAttack attack;
    private float previousFrameTime;
    private bool alReadyAppliedForce;

    public EliteMeleeAttackState(EliteMeleeStateMachine stateMachine, int AttackIndex) : base(stateMachine) {

        attack = stateMachine.Attacks[AttackIndex];
    }

    public override void Enter() {

        stateMachine.Rigidbody.velocity = Vector3.zero;

        //setting attack value to weapondamage script base on attack class
        stateMachine.Weapon.SetAttack(attack.AttackDamage, attack.AttackKnockback, attack.AttackUpKnockback);

        //playing some animation with transition
        stateMachine.Animator.CrossFadeInFixedTime(attack.AnimationName, attack.TransitionDuration);
    }

    public override void Tick(float deltaTime) {

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

            if (!IsInAttackRange()) {

                stateMachine.SwitchState(new EliteMeleeMainState(stateMachine));
                return;
            }
        }
        previousFrameTime = normalizedTime;
    }

    public override void Exit() {}

    private void TryComboAttack(float normalizedTime) {

        if (attack.ComboStateIndex == -1) { return; }

        if (normalizedTime < attack.ComboAttackTime) { return; }

        //stateMachine.SwitchState(new EliteMeleeAttackState(stateMachine, attack.ComboStateIndex));
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
