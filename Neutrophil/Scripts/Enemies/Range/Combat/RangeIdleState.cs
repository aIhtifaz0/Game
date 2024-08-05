using UnityEngine;

public class RangeIdleState : RangeBaseState {

    public RangeIdleState(RangeStateMachine stateMachine) : base(stateMachine) {}

    private readonly int MovementHash = Animator.StringToHash("MovementBlendTree");
    private readonly int MovementSpeed = Animator.StringToHash("MovementSpeed");

    private const float CrossFadeDuration = 0.1f;
    private const float AnimatorDampTime = 0.1f;

    private float AttackTimer;
    private float timerBetweenAttack = 1.3f;

    private float duration;

    public override void Enter() {

        duration = Random.Range(1f, 3f);
        stateMachine.IsShooting = true;

        AttackTimer = timerBetweenAttack;

        //setting to movement blendtree
        stateMachine.Animator.CrossFadeInFixedTime(MovementHash, CrossFadeDuration);
    }

    public override void Tick(float deltatime) {

        stateMachine.Rigidbody.velocity = Vector2.zero;

        CheckPlayerDashing();

        if (duration <= 0f && stateMachine.RangeType != RangeStateMachine.Range.StayRange)
        {
            stateMachine.SwitchState(new RangePatrolState(stateMachine));
        }
        else
        {
            duration -= deltatime;
            stateMachine.Animator.SetFloat(MovementSpeed, 0f, AnimatorDampTime, deltatime);
        }

        if (IsInSpotRange() && stateMachine.RangeType != RangeStateMachine.Range.StayRange) {

            stateMachine.SwitchState(new RangeMainState(stateMachine));
            return;
        }
        else{

            CheckFacing();
        }

        if (IsInSpotRange() && stateMachine.RangeType == RangeStateMachine.Range.StayRange) {

            if(stateMachine.IsShooting){
                stateMachine.SwitchState(new RangeShootState(stateMachine));
                return;
            }
        }

        if (stateMachine.RangeType == RangeStateMachine.Range.StayRange) {

            stateMachine.Animator.SetFloat(MovementSpeed, 0f, AnimatorDampTime, deltatime);
        }
    }

    public override void Exit() {}
}