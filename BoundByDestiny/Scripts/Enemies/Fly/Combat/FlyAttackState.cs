using UnityEngine;

public class FlyAttackState : FlyBaseState{

    private FlyAttack attack;
    private float previousFrameTime;
    private bool alReadyAppliedForce;
    private float timer;
    private float attackTime = 1f;

    public FlyAttackState(FlyStateMachine stateMachine, int AttackIndex) : base(stateMachine) {

        attack = stateMachine.Attacks;
    }

    public override void Enter() {

        stateMachine.Audio.PlaySound(stateMachine.Audio.Attack);
        stateMachine.Rigidbody.velocity = Vector3.zero;
        stateMachine.StartCoroutine(stateMachine.FlyMeleeAttack());

        timer = attackTime;

        //setting attack value to weapondamage script base on attack class
        stateMachine.Weapon.SetAttack(attack.AttackDamage, attack.AttackKnockback, 0f);

        //playing some animation with transition
        //stateMachine.Animator.CrossFadeInFixedTime(attack.AnimationName, attack.TransitionDuration);
    }

    public override void Tick(float deltaTime) {

/*
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
*/

        timer -= deltaTime;

        if(timer <= 0f){

             if (IsInSpotRange()) {

                stateMachine.SwitchState(new FlyChaseState(stateMachine));
                return;
            }
        }
        //previousFrameTime = normalizedTime;
    }

    public override void Exit() {
        stateMachine.IsAttacking = false;
    }

    private void TryComboAttack(float normalizedTime) {

        if (attack.ComboStateIndex == -1) { return; }

        if (normalizedTime < attack.ComboAttackTime) { return; }

        stateMachine.SwitchState(new FlyAttackState(stateMachine, attack.ComboStateIndex));
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
