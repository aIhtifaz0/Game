using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeJumpAttackState : MeleeBaseState{

    private JumpAttack attack;
    private float previousFrameTime;
    private float distance;
     private float duration = 1f;
    private bool alReadyAppliedForce;

    public MeleeJumpAttackState(MeleeStateMachine stateMachine, int AttackIndex) : base(stateMachine) {

        attack = stateMachine.JumpAttack;
    }

    public override void Enter(){

        stateMachine.Audio.PlaySound(stateMachine.Audio.Jump);

        distance = stateMachine.astralStateMachine.transform.position.x - stateMachine.transform.position.x;

        if(stateMachine.IsGrounded){
            stateMachine.Rigidbody.AddForce(new Vector2(distance, attack.JumpForce), ForceMode2D.Impulse);
        }

         //setting attack value to weapondamage script base on attack class
        stateMachine.Weapon.SetAttack(attack.AttackDamage, attack.AttackKnockback, attack.AttackUpKnockback);

        //playing some animation with transition
        stateMachine.Animator.CrossFadeInFixedTime(attack.AnimationName, attack.TransitionDuration);
    }

    public override void Tick(float deltatime){

        CheckFacing();

        if(stateMachine.AstralMode.IsAstral){
            
            CheckAstralDashing();
        }
        else if(!stateMachine.AstralMode.IsAstral){

            CheckPlayerDashing();
        }

        duration -= Time.deltaTime;

        float normalizedTime = GetNormalizedTime(stateMachine.Animator, "Attack");

        if (normalizedTime < 1f) {

            //adding force to forward when attack
            if (normalizedTime >= attack.ForceTime) {

                TryApplyForce();
            }
        }
        else {

            if(duration <= 0f){

                if(stateMachine.MeleeType == MeleeStateMachine.Melee.StrongMelee){
                    stateMachine.IsRecharging = true;
                    stateMachine.SwitchState(new MeleeRechargeState(stateMachine));
                    return;
                }
                if(stateMachine.MeleeType == MeleeStateMachine.Melee.JumpMelee){
                    stateMachine.IsRecharging = true;
                    stateMachine.SwitchState(new MeleeRechargeState(stateMachine));
                }
            }
        }
        previousFrameTime = normalizedTime;
    }

    public override void Exit(){}

    private void TryApplyForce() {

        if (alReadyAppliedForce) { return; }

        Vector3 Dir = stateMachine.IsFacingRight ? Vector3.right : Vector3.left; 

        stateMachine.Rigidbody.AddForce(Dir * attack.ForwardForce, ForceMode2D.Force);
        alReadyAppliedForce = true;
    }
}
