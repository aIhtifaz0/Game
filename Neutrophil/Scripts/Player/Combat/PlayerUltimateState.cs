using UnityEngine;

public class PlayerUltimateState : PlayerBaseState{

    public PlayerUltimateState(PlayerStateMachine stateMachine) : base(stateMachine) {}

    //Convert String to Hash for Animation
    private readonly int UltimateHash = Animator.StringToHash("Ultimate");
    private const float TransitionDuration = 0.1f;
    private float duration = 3f;

    public override void Enter(){

        stateMachine.Ultimate--;
        //playing impact animation when it start
        stateMachine.Animator.CrossFadeInFixedTime(UltimateHash, TransitionDuration);
    }

    public override void Tick(float deltaTime){

        //After hit duration pass back to previous state
        duration -= Time.deltaTime;

        if (stateMachine.InputReader.IsBasicAttacking) {

            stateMachine.Rigidbody.velocity = Vector3.zero;
            stateMachine.SwitchState(new PlayerMainUltimateAttackState(stateMachine));
            return;
        }
        if (stateMachine.InputReader.IsSecondaryAttacking) {

            stateMachine.Rigidbody.velocity = Vector3.zero;
            stateMachine.SwitchState(new PlayerSecondaryUltimateAttackState(stateMachine));
            return;
        }
        if (stateMachine.InputReader.IsTertiaryAttacking) {

            stateMachine.Rigidbody.velocity = Vector3.zero;
            stateMachine.SwitchState(new PlayerTertiaryUltimateAttackState(stateMachine));
            return;
        }

        if(duration <= 0f){

            if (stateMachine.Rigidbody.velocity.y < 0 || !stateMachine.IsGrounded) {
                stateMachine.SwitchState(new PlayerFallState(stateMachine));
                return;
            }
            if (stateMachine.IsGrounded) {
                stateMachine.SwitchState(new PlayerMainState(stateMachine));
            }
        }
    } 

    public override void Exit() {}
}
