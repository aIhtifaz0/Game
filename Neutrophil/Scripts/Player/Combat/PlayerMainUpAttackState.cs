using UnityEngine;

public class PlayerMainUpAttackState : PlayerBaseState {

    private StrikeUpAttackData attack;
    private bool alReadyAppliedForce;
    public PlayerMainUpAttackState(PlayerStateMachine stateMachine) : base(stateMachine) {

        attack = stateMachine.MainWeapon.StrikeUpAttack;

        stateMachine.InputReader.DisablingAttack();
    }

    public override void Enter() {
        stateMachine.Rigidbody.velocity = Vector3.zero;
        stateMachine.LastOnGroundTime = stateMachine.ControllerData.coyoteTime;

        stateMachine.Audio.PlaySound(stateMachine.Audio.Attack);
        stateMachine.Weapon.SetAttack(attack.Damage, attack.Knockback, attack.UpKnockback, 0f, 0f);
        stateMachine.Animator.CrossFadeInFixedTime(attack.AnimationName, attack.TransitionDuration);
    }

    public override void Tick(float deltatime) {

        float normalizedTime = GetNormalizedTime(stateMachine.Animator, "Attack");

        if (normalizedTime < 0.5f) {

            if (stateMachine.InputReader.IsAirAttacking) {

                TryComboAttack(normalizedTime);
            }
        }
        else {

            TryApplyForce();
            CheckFacing();

            if (stateMachine.IsGrounded){
                stateMachine.SwitchState(new PlayerMainState(stateMachine));
                return;
            }

            if (stateMachine.Rigidbody.velocity.y < 0) {
                stateMachine.SwitchState(new PlayerFallState(stateMachine));
            }
        }
    }

    public override void Exit() {

        // stateMachine.IsJumping = true;
        // stateMachine.IsGrounded = false;
    }
    private void TryApplyForce() {

        if (alReadyAppliedForce) { return; }

        stateMachine.Rigidbody.AddForce(Vector2.up * attack.Force, ForceMode2D.Force);
        alReadyAppliedForce = true;
    }


    private void TryComboAttack(float normalizedTime) {

        if (attack.ComboStateIndex == -1) { return; }

        if (normalizedTime < attack.ComboAttackTime) { return; }

        stateMachine.SwitchState(new PlayerMainUpAttackState(stateMachine));
    }
}

/*
    getting attack index from last frame or input and resetting all attack boolean
    set the lastOngroundTime to coyotetime for set the player on the ground
    resetting the velocity, setting attack damage, knockback and animation.
    setting no gravity, setting attack upward force at start of the scipt for do it once and checking if try to combo/attack again
    if pass checking back if player try to downwardstrike or falling and then switch the state

    TryApplyForce : adding rigidbody force based on attack direction and force power

    TryComboAttack : checking if still any attack index available and back to attack if there is available index or reset if no any available index
    
*/
