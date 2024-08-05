using UnityEngine;

public class FlyDeathState : FlyBaseState {

    public FlyDeathState(FlyStateMachine stateMachine) : base(stateMachine) {}

    //Convert String to Hash for Animation
    private readonly int ImpactHash = Animator.StringToHash("Death");
    private const float TransitionDuration = 0.1f;

    public override void Enter() {

        //playing impact animation when it start
        stateMachine.Animator.CrossFadeInFixedTime(ImpactHash, TransitionDuration);

        stateMachine.Rigidbody.velocity = Vector3.zero;

        //disable weapon collider
        stateMachine.Weapon.gameObject.SetActive(false);

        Object.Destroy(stateMachine.gameObject, 1f);
    }

    public override void Tick(float deltatime) {

    }

    public override void Exit() {}
}
