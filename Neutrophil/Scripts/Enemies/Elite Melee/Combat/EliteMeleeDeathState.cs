using UnityEngine;

public class EliteMeleeDeathState : EliteMeleeBaseState {

    public EliteMeleeDeathState(EliteMeleeStateMachine stateMachine) : base(stateMachine) {}

    //Convert String to Hash for Animation
    private readonly int ImpactHash = Animator.StringToHash("Death");
    private const float TransitionDuration = 0.1f;

    public override void Enter() {

        stateMachine.Rigidbody.velocity = Vector3.zero;
        
        //playing impact animation when it start
        stateMachine.Animator.CrossFadeInFixedTime(ImpactHash, TransitionDuration);

        //disable weapon collider
        stateMachine.Weapon.gameObject.SetActive(false);

        Object.Destroy(stateMachine.gameObject, 1f);
    }

    public override void Tick(float deltatime) {

        CheckPlayerDashing();
    }

    public override void Exit() {}
}
