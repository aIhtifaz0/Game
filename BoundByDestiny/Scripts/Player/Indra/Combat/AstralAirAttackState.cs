using UnityEngine;

public class AstralAirAttackState : AstralBaseState{

    private BasicAttackData attack;
    private bool alReadyAppliedForce;

    public AstralAirAttackState(AstralStateMachine stateMachine, int AttackIndex) : base(stateMachine){

        attack = stateMachine.UpAttack;

        stateMachine.AstralInputReader.DisablingAttack();
    }

    public override void Enter(){

        stateMachine.Audio.PlaySound(stateMachine.Audio.Attack);
        stateMachine.Rigidbody.velocity = Vector3.zero;

        stateMachine.Weapon.SetAttack(attack.Damage, attack.Knockback);
        stateMachine.Animator.CrossFadeInFixedTime(attack.AnimationName, attack.TransitionDuration);
    }

    public override void Tick(float deltatime)
    {
        stateMachine.ForceReceiver.SetGravityScale(0);

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
            stateMachine.ForceReceiver.SetGravityScale(stateMachine.ControllerData.GravityScale);

            if (stateMachine.Rigidbody.velocity.y < 0.01f || !stateMachine.IsJumping) {
                stateMachine.SwitchState(new AstralFallState(stateMachine));
                return;
            }
            if (stateMachine.IsGrounded) {
                stateMachine.SwitchState(new AstralMainState(stateMachine));
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

        stateMachine.SwitchState(new AstralMainBasicAttackState(stateMachine, attack.ComboStateIndex));
    }
}
