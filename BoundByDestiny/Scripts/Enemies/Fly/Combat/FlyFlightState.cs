using UnityEngine;

public class FlyFlightState : FlyBaseState {

    public FlyFlightState(FlyStateMachine stateMachine) : base(stateMachine) {}

    /*
    private readonly int FlightMovement = Animator.StringToHash("Movement");
    private readonly int Speed = Animator.StringToHash("FlightSpeed");

    private const float CrossFadeDuration = 0.1f;
    private const float AnimatorDampTime = 0.1f;
    */
    private float duration;

    public override void Enter() {

        duration = Random.Range(1f, 3f);

        //setting to movement blendtree
        //stateMachine.Animator.CrossFadeInFixedTime(FlightMovement, CrossFadeDuration);
    }

    public override void Tick(float deltatime) {

        if (duration <= 0f)
        {
            stateMachine.SwitchState(new FlyPatrolState(stateMachine));
        }
        else
        {
            duration -= deltatime;
        }

        if (IsInSpotRange()) {

            stateMachine.SwitchState(new FlyChaseState(stateMachine));
            return;
        }

        //stateMachine.Animator.SetFloat(Speed, 0f, AnimatorDampTime, deltatime);
    }

    public override void Exit() {}
}