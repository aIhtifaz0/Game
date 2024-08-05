using UnityEngine;

public class PlayerAirAttackState : PlayerBaseState{

    private BasicAttackData attack;
    private bool alReadyAppliedForce;
    public PlayerAirAttackState(PlayerStateMachine stateMachine, int AttackIndex) : base(stateMachine){

        attack = stateMachine.AirUpAttack;

        stateMachine.InputReader.DisablingAttack();
    }

    public override void Enter(){

        stateMachine.Audio.PlaySound(stateMachine.Audio.Attack);

        stateMachine.Rigidbody.velocity = Vector3.zero;

        stateMachine.Weapon.SetAttack(attack.Damage, attack.Knockback);
        stateMachine.Animator.CrossFadeInFixedTime(attack.AnimationName, attack.TransitionDuration);
    }

    public override void Tick(float deltatime)
    {
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
            stateMachine.ForceReceiver.SetGravityScale(stateMachine.ControllerData.GravityScale);

            if (stateMachine.Rigidbody.velocity.y < 0.01f || !stateMachine.IsJumping) {
                stateMachine.SwitchState(new PlayerFallState(stateMachine));
                return;
            }
            if (stateMachine.IsGrounded) {
                stateMachine.SwitchState(new PlayerMainState(stateMachine));
            }
        }
    }

    public override void Exit() {

        stateMachine.IsJumping = false;
    }

     private void TryApplyForce() {

        if (alReadyAppliedForce) { return; }

        stateMachine.Rigidbody.AddForce(Vector2.up * attack.Force, ForceMode2D.Force);
        alReadyAppliedForce = true;
    }

    private void TryComboAttack(float normalizedTime) {

        if (attack.ComboStateIndex == -1) { return; }

        if (normalizedTime < attack.ComboAttackTime) { return; }

        stateMachine.SwitchState(new PlayerAirAttackState(stateMachine, attack.ComboStateIndex));
    }
}
