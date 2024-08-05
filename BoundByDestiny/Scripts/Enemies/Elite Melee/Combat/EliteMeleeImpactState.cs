using UnityEngine;

public class EliteMeleeImpactState : EliteMeleeBaseState{

    public EliteMeleeImpactState(EliteMeleeStateMachine stateMachine) : base(stateMachine) {}

    //Convert String to Hash for Animation
    private readonly int ImpactHash = Animator.StringToHash("Impact");
    private const float TransitionDuration = 0.1f;
    private float duration = 0.7f;

    public override void Enter() {

        //playing impact animation when it start
        stateMachine.Animator.CrossFadeInFixedTime(ImpactHash, TransitionDuration);
    }

    public override void Tick(float deltatime) {

        //After hit duration pass back to previous state
        duration -= deltatime;

        if(stateMachine.AstralMode.IsAstral){
            
            CheckAstralDashing();
        }
        else if(!stateMachine.AstralMode.IsAstral){

            CheckPlayerDashing();
        }
        
       if (duration <= 0f) {

            if (stateMachine.IsGrounded) {
                stateMachine.SwitchState(new EliteMeleeMainState(stateMachine));
                return;
            }
        }
    }

    public override void Exit() {}
}
