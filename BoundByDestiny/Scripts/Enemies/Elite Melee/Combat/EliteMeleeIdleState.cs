using UnityEngine;

public class EliteMeleeIdleState : EliteMeleeBaseState {

    public EliteMeleeIdleState(EliteMeleeStateMachine stateMachine) : base(stateMachine) {}

    private readonly int MovementHash = Animator.StringToHash("MovementBlendTree");
    private readonly int MovementSpeed = Animator.StringToHash("MovementSpeed");

    private const float CrossFadeDuration = 0.1f;
    private const float AnimatorDampTime = 0.1f;

    private float duration;

    public override void Enter() {

        duration = Random.Range(1f, 3f);

        //setting to movement blendtree
        stateMachine.Animator.CrossFadeInFixedTime(MovementHash, CrossFadeDuration);
    }

    public override void Tick(float deltatime) {

        stateMachine.Rigidbody.velocity = Vector2.zero;

        if(stateMachine.AstralMode.IsAstral){
            
            CheckAstralDashing();
        }
        else if(!stateMachine.AstralMode.IsAstral){

            CheckPlayerDashing();
        }
        

        if (duration <= 0f)
        {
            stateMachine.SwitchState(new EliteMeleePatrolState(stateMachine));
        }
        else
        {
            duration -= deltatime;
        }

        if (IsInSpotRange()) {

            stateMachine.SwitchState(new EliteMeleeMainState(stateMachine));
            return;
        }

        stateMachine.Animator.SetFloat(MovementSpeed, 0f, AnimatorDampTime, deltatime);
    }

    public override void Exit() {}
}