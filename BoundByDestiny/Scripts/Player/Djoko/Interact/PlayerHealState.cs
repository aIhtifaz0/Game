using UnityEngine;

public class PlayerHealState : PlayerBaseState{

    public PlayerHealState(PlayerStateMachine stateMachine) : base(stateMachine) {}

    //Convert String to Hash for Animation
    private readonly int HealHash = Animator.StringToHash("Heal");
    private const float TransitionDuration = 0.1f;
    private float duration = 0.6f;
    
    public override void Enter(){

        stateMachine.Rigidbody.velocity = Vector3.zero;

        //playing impact animation when it start
        stateMachine.Animator.CrossFadeInFixedTime(HealHash, TransitionDuration);
    }

    public override void Tick(float deltaTime){

        //After hit duration pass back to previous state
        duration -= Time.deltaTime;

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
