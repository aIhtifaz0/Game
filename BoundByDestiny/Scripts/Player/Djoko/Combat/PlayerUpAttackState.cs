using UnityEngine;

public class PlayerUpAttackState : PlayerBaseState{
    
    private BasicAttackData attack;
    private bool alReadyAppliedForce;
    public PlayerUpAttackState(PlayerStateMachine stateMachine, int AttackIndex) : base(stateMachine) {
        
        attack = stateMachine.UpAttack;

        stateMachine.InputReader.DisablingAttack();
    }

    public override void Enter() {

        stateMachine.Audio.PlaySound(stateMachine.Audio.Attack);
        stateMachine.Rigidbody.velocity = Vector3.zero;

        stateMachine.Weapon.SetAttack(attack.Damage, attack.Knockback);
        stateMachine.Animator.CrossFadeInFixedTime(attack.AnimationName, attack.TransitionDuration);
    }

    public override void Tick(float deltatime) {
        if (!stateMachine.IsDashing && !stateMachine.IsJumping)
        {
            //Ground Check
            if (Physics2D.OverlapBox(stateMachine._groundCheckPoint.position, stateMachine._groundCheckSize, 0, stateMachine._groundLayer) && !stateMachine.IsJumping) //checks if set box overlaps with ground
            {
                stateMachine.LastOnGroundTime = stateMachine.ControllerData.coyoteTime; //if so sets the lastGrounded to coyoteTime
                stateMachine.IsGrounded = true;
            }
        }

        stateMachine.ForceReceiver.SetGravityScale(0);

        float normalizedTime = GetNormalizedTime(stateMachine.Animator, "Attack");

        if (normalizedTime < 1f) {

            if (normalizedTime >= attack.ForceTime) {

                TryApplyForce();
            }

            if (stateMachine.InputReader.IsAttacking) {

                TryComboAttack(normalizedTime);
            }
        }
        else {
            stateMachine.IsGrounded = true;
            if (stateMachine.Rigidbody.velocity.y < 0) {
                stateMachine.SwitchState(new PlayerFallState(stateMachine));
                return;
            }
            if (stateMachine.IsGrounded) {
                stateMachine.SwitchState(new PlayerMainState(stateMachine));
            }
        }
    }

    public override void Exit() {}

    private void TryApplyForce() {

        if (alReadyAppliedForce) { return; }

        stateMachine.Rigidbody.AddForce(Vector2.up * attack.Force, ForceMode2D.Force);
        alReadyAppliedForce = true;
    }

    private void TryComboAttack(float normalizedTime) {

        if (attack.ComboStateIndex == -1) { return; }

        if (normalizedTime < attack.ComboAttackTime) { return; }

        stateMachine.SwitchState(new PlayerUpAttackState(stateMachine, attack.ComboStateIndex));
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
