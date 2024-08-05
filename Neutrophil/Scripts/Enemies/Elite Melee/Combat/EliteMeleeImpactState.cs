using UnityEngine;

public class EliteMeleeImpactState : EliteMeleeBaseState{

    public EliteMeleeImpactState(EliteMeleeStateMachine stateMachine) : base(stateMachine) {}

    //Convert String to Hash for Animation
    private readonly int ImpactHash = Animator.StringToHash("Impact");
    private const float TransitionDuration = 0.1f;
    private float duration = 0.3f;

    public override void Enter() { 

        stateMachine.Rigidbody.velocity = Vector3.zero;
        stateMachine.Rigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;

        //playing impact animation when it start
        stateMachine.Animator.CrossFadeInFixedTime(ImpactHash, TransitionDuration);
    }

    public override void Tick(float deltatime) {

        //After hit duration pass back to previous state
        duration -= deltatime;

        CheckPlayerDashing();
        
       if (duration <= 0f) {

        Debug.Log(stateMachine.IsGrounded);

            if (stateMachine.IsGrounded) {
                stateMachine.SwitchState(new EliteMeleeMainState(stateMachine));
                return;
            }
        }
    }

    public override void Exit() {}
}
