using UnityEngine;

public class AstralImpactState : AstralBaseState{

    public AstralImpactState(AstralStateMachine stateMachine) : base(stateMachine) {}

    //Convert String to Hash for Animation
    private readonly int ImpactHash = Animator.StringToHash("Impact");
    private const float TransitionDuration = 0.1f;
    private float duration = 0.6f;
    
    public override void Enter(){

        //playing impact animation when it start
        stateMachine.Animator.CrossFadeInFixedTime(ImpactHash, TransitionDuration);
    }

    public override void Tick(float deltaTime){

        //After hit duration pass back to previous state
        duration -= Time.deltaTime;

        if(duration <= 0f){

            if (stateMachine.Rigidbody.velocity.y < 0 || !stateMachine.IsGrounded) {
                stateMachine.SwitchState(new AstralFallState(stateMachine));
                return;
            }
            if (stateMachine.IsGrounded) {
                stateMachine.SwitchState(new AstralMainState(stateMachine));
            }
        }
    } 

    public override void Exit() {}
}
