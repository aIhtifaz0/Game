using UnityEngine;

public class MeleeImpactState : MeleeBaseState{

    public MeleeImpactState(MeleeStateMachine stateMachine) : base(stateMachine) {}

    //Convert String to Hash for Animation
    private readonly int ImpactHash = Animator.StringToHash("Impact");
    private const float TransitionDuration = 0.1f;
    private float duration = 0.5f;

    public override void Enter() {

        //playing impact animation when it start
        stateMachine.Animator.CrossFadeInFixedTime(ImpactHash, TransitionDuration);
    }

    public override void Tick(float deltatime) {

        //After hit duration pass back to previous state
        stateMachine.StunDuraion -= deltatime;
        
       if (stateMachine.StunDuraion <= 0f) {

            if (stateMachine.IsGrounded) {
                stateMachine.SwitchState(new MeleeSpotState(stateMachine));
                return;
            }
        }
    }

    public override void Exit() {}
}
