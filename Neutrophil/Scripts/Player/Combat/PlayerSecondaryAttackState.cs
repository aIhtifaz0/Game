using UnityEngine;

public class PlayerSecondaryAttackState : PlayerBaseState{

    private SecondaryAttackData attack;
    private bool alReadyAppliedForce;

    public PlayerSecondaryAttackState(PlayerStateMachine stateMachine, int AttackIndex) : base(stateMachine) {

        attack = stateMachine.SecondaryWeapon.SecondaryAttack[AttackIndex];

        stateMachine.InputReader.DisablingAttack();
    }

    public override void Enter() {

        stateMachine.Rigidbody.velocity = Vector3.zero;

        stateMachine.Audio.PlaySound(stateMachine.Audio.Attack);
        stateMachine.Weapon.SetAttack(attack.Damage, attack.Knockback, 0f, 0f, attack.BackKnockback);
        stateMachine.Animator.CrossFadeInFixedTime(attack.AnimationName, attack.TransitionDuration);
    }

    public override void Tick(float deltatime) {

        stateMachine.ForceReceiver.SetGravityScale(0);

        float normalizedTime = GetNormalizedTime(stateMachine.Animator, "Attack");

        if (normalizedTime < 1f) {

            if (normalizedTime >= attack.ForceTime) {

                TryApplyForce();
            }

            if (stateMachine.InputReader.IsSecondaryAttacking) {

                TryComboAttack(normalizedTime);
            }
        }
        else {
            stateMachine.ForceReceiver.SetGravityScale(stateMachine.ControllerData.GravityScale);

            if (stateMachine.Rigidbody.velocity.y < 0.01f || !stateMachine.IsJumping) {
                stateMachine.SwitchState(new PlayerFallState(stateMachine));
                return;
            }
            if (stateMachine.IsGrounded == true) {
                stateMachine.SwitchState(new PlayerMainState(stateMachine));
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

        stateMachine.SwitchState(new PlayerSecondaryAttackState(stateMachine, attack.ComboStateIndex));
    }
}
/*
    getting attack index from last frame or input and resetting all attack boolean
    resetting the velocity, setting attack damage, knockback and animation.
    setting no gravity, setting attack forward force and checking if try to combo/attack again
    if pass checking back to falling or in ground to switch back state.

    TryApplyForce : adding rigidbody force based on attack direction and force power

    TryComboAttack :  checking if still any attack index available and back to attack if there is available index or reset if no any available index
    
*/
