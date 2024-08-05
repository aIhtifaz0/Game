using UnityEngine;

public class PlayerMainUltimateAttackState : PlayerBaseState{

    private MainUltimateAttackData attack;
    private bool alReadyAppliedForce;
    private float duration = 1f;

    public PlayerMainUltimateAttackState(PlayerStateMachine stateMachine) : base(stateMachine) {

        attack = stateMachine.MainWeapon.UltimateAttack;

        stateMachine.InputReader.DisablingAttack();
    }

    public override void Enter() {

        stateMachine.Stats.SetInvulnerable(true);

        stateMachine.Rigidbody.velocity = Vector3.zero;

        stateMachine.Weapon.SetAttack(attack.Damage, attack.Knockback, attack.UpKnockback, 0f, 0f);
        stateMachine.Animator.CrossFadeInFixedTime(attack.AnimationName, attack.TransitionDuration);
    }

    public override void Tick(float deltatime) {

        stateMachine.ForceReceiver.SetGravityScale(0);

        duration -= Time.deltaTime;

        stateMachine.ForceReceiver.SetGravityScale(stateMachine.ControllerData.GravityScale);

        float normalizedTime = GetNormalizedTime(stateMachine.Animator, "Attack");

        if (normalizedTime < 1f) {

            if (normalizedTime >= attack.ForceTime) {

                TryApplyForce();
            }
        }

        if(duration <= 0f){

            if (stateMachine.Rigidbody.velocity.y < 0.01f || !stateMachine.IsJumping) {
                stateMachine.SwitchState(new PlayerFallState(stateMachine));
                return;
            }
            if (stateMachine.IsGrounded == true) {
                stateMachine.SwitchState(new PlayerMainState(stateMachine));
            }
        }
    }

    public override void Exit() { 

        stateMachine.Stats.SetInvulnerable(false);
    }

    private void TryApplyForce() {

        if (alReadyAppliedForce) { return; }

        Vector3 playerDir = stateMachine.IsFacingRight ? Vector3.right : Vector3.left;

        stateMachine.Rigidbody.AddForce(playerDir * attack.Force, ForceMode2D.Force);
        alReadyAppliedForce = true;
    }
}
