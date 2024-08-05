using UnityEngine;

public class PlayerMainDownAttackState : PlayerBaseState {

    private StrikeDownAttackData attack;
    private bool alReadyAppliedForce;
    public PlayerMainDownAttackState(PlayerStateMachine stateMachine) : base(stateMachine) {

        attack = stateMachine.MainWeapon.StrikeDownAttack;

        stateMachine.InputReader.DisablingAttack();
    }

    public override void Enter() {

        stateMachine.Stats.SetInvulnerable(true);

        stateMachine.Audio.PlaySound(stateMachine.Audio.Attack);
        stateMachine.Rigidbody.velocity = Vector3.zero;
        stateMachine.LastOnGroundTime = stateMachine.ControllerData.coyoteTime;

        stateMachine.Weapon.SetAttack(attack.Damage, attack.Knockback, 0f, attack.DownKnockback, 0f);
        stateMachine.Animator.CrossFadeInFixedTime(attack.AnimationName, attack.TransitionDuration);
    }

    public override void Tick(float deltatime) {

        TryApplyForce();

        float normalizedTime = GetNormalizedTime(stateMachine.Animator, "Attack");

        if (normalizedTime < 1f) {

            if (stateMachine.InputReader.IsBasicAttacking) {

                TryComboAttack(normalizedTime);
            }
        }
        else {

            CheckFacing();
            #region Collision Check
            if (Physics2D.OverlapBox(stateMachine._groundCheckPoint.position, stateMachine._groundCheckSize, 0, stateMachine._groundLayer) && stateMachine.IsJumping) //checks if set box overlaps with ground
               {
                stateMachine.LastOnGroundTime = stateMachine.ControllerData.coyoteTime; //if so sets the lastGrounded to coyoteTime
                stateMachine.IsGrounded = true;
            }
            #endregion

            if (stateMachine.IsGrounded == true || !stateMachine.IsJumping) {

                stateMachine.SwitchState(new PlayerMainState(stateMachine));
            }
        }
    }

    public override void Exit() {
        
        stateMachine.IsJumping = false;
        stateMachine.Stats.SetInvulnerable(false);
    }
    private void TryApplyForce() {

        if (alReadyAppliedForce) { return; }

        

        stateMachine.Rigidbody.AddForce(Vector2.down * attack.Force, ForceMode2D.Force);
        alReadyAppliedForce = true;
    }


    private void TryComboAttack(float normalizedTime) {

        if (attack.ComboStateIndex == -1) { return; }

        if (normalizedTime < attack.ComboAttackTime) { return; }

        stateMachine.SwitchState(new PlayerMainBasicAttackState(stateMachine, attack.ComboStateIndex));
    }
}

/*
    getting attack index from last frame or input and resetting all attack boolean
    set the lastOngroundTime to coyotetime because set the player on the ground
    resetting the velocity, setting attack damage, knockback and animation.
    setting no gravity, setting attack downward force and checking if try to combo/attack again
    if pass checking back if player on the ground and switchback to main state

    TryApplyForce : adding rigidbody force based on attack direction and force power

    TryComboAttack : checking if still any attack index available and back to attack if there is available index or reset if no any available index
    
*/
