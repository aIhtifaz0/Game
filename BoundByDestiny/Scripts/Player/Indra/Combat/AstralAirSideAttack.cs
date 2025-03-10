using UnityEngine;

public class AstralAirSideAttackState : AstralBaseState{
    
    private BasicAttackData attack;
    private bool alReadyAppliedForce;
    public AstralAirSideAttackState(AstralStateMachine stateMachine, int AttackIndex) : base(stateMachine) {
        
        attack = stateMachine.AirAttack;

        stateMachine.AstralInputReader.DisablingAttack();
    }

    public override void Enter() {

        stateMachine.Audio.PlaySound(stateMachine.Audio.Attack);
        stateMachine.Rigidbody.velocity = Vector3.zero;
        stateMachine.ForceReceiver.SetGravityScale(0);
        stateMachine.Weapon.SetAttack(attack.Damage, attack.Knockback);
        stateMachine.Animator.CrossFadeInFixedTime(attack.AnimationName, attack.TransitionDuration);
    }

    public override void Tick(float deltatime) {

        float normalizedTime = GetNormalizedTime(stateMachine.Animator, "Attack");

        if (normalizedTime < 1f) {

            if (normalizedTime >= attack.ForceTime) {

                TryApplyForce();
            }

            if (stateMachine.AstralInputReader.IsAttacking) {

                TryComboAttack(normalizedTime);
            }
        }
        else {
            stateMachine.IsGrounded = true;
            if (stateMachine.Rigidbody.velocity.y < 0) {
                stateMachine.SwitchState(new AstralFallState(stateMachine));
                return;
            }
            if (stateMachine.IsGrounded) {
                stateMachine.SwitchState(new AstralMainState(stateMachine));
            }
        }
    }

    public override void Exit() {}

    private void TryApplyForce() {

        if (alReadyAppliedForce) { return; }

        Vector3 playerDir = stateMachine.IsFacingRight ? Vector3.right : Vector3.left; 

        stateMachine.Rigidbody.AddForce(playerDir * attack.Force, ForceMode2D.Force);
        alReadyAppliedForce = true;
    }

    private void TryComboAttack(float normalizedTime) {

        if (attack.ComboStateIndex == -1) { return; }

        if (normalizedTime < attack.ComboAttackTime) { return; }

        stateMachine.SwitchState(new AstralAirSideAttackState(stateMachine, attack.ComboStateIndex));
    }
}

/*
    getting attack index from last frame or input and resetting all attack boolean
    resetting the velocity, setting attack damage, knockback and animation.
    setting no gravity, setting attack forward force and checking if try to combo/attack again
    if pass checking back to falling or in ground to switch back state.

    TryApplyForce : adding rigidbody force based on attack direction and force power

    TryComboAttack :    checking if still any attack index available and back to attack if there is available index or reset if no any available index
    
*/
