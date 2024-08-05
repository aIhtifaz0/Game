using UnityEngine;

public class FlyImpactState : FlyBaseState{

    public FlyImpactState(FlyStateMachine stateMachine) : base(stateMachine) {}

    /*
    //Convert String to Hash for Animation
    private readonly int ImpactHash = Animator.StringToHash("Hurt");
    private const float TransitionDuration = 0.1f;
    
    */
    private float duration = 0.7f;

    public override void Enter() {

        //playing impact animation when it start
        //stateMachine.Animator.CrossFadeInFixedTime(ImpactHash, TransitionDuration);
    }

    public override void Tick(float deltatime) {

        //After hit duration pass back to previous state
        duration -= deltatime;

        if (Physics2D.OverlapBox(stateMachine._groundCheckPoint.position, stateMachine._groundCheckSize, 0, stateMachine._groundLayer))
        { //checks if set box overlaps with ground >> && stateMachine.IsJumping bug IsGrounded cannot true

            stateMachine.IsGrounded = true;
        }
        if (duration <= 0f) {

           if (stateMachine.IsGrounded) {

                stateMachine.SwitchState(new FlyChaseState(stateMachine));
                return;
            }
        }
    }

    public override void Exit() {}
}
