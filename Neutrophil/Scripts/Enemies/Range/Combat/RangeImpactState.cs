using UnityEngine;

public class RangeImpactState : RangeBaseState{

    public RangeImpactState(RangeStateMachine stateMachine) : base(stateMachine) {}

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

        CheckPlayerDashing();
        
        if (duration <= 0f) {

            if(stateMachine.IsReloading) {
                stateMachine.SwitchState(new RangeReloadState(stateMachine));
                return;
            }

            if (!stateMachine.IsReloading) {

                if (IsInShootRange()) {

                    if(stateMachine.IsAttacking){
                        stateMachine.SwitchState(new RangeShootState(stateMachine));
                        return;
                    }
                }

                if(IsInAttackRange()){

                    if(stateMachine.IsAttacking && stateMachine.RangeType == RangeStateMachine.Range.MeleeRange){
                        stateMachine.SwitchState(new RangeAttackState(stateMachine, 0));
                    }
                    if(stateMachine.IsChargeAttacking && stateMachine.RangeType == RangeStateMachine.Range.EliteRange){
                        stateMachine.SwitchState(new RangeAttackState(stateMachine, 0));
                    }
                }

                if (IsInSpotRange() && stateMachine.RangeType != RangeStateMachine.Range.StayRange) {
                    stateMachine.SwitchState(new RangeMainState(stateMachine));
                    return;
                }

                if (IsInSpotRange() && stateMachine.RangeType == RangeStateMachine.Range.StayRange) {
                    stateMachine.SwitchState(new RangeIdleState(stateMachine));
                    return;
                }

                if(!IsInSpotRange()){
                    stateMachine.SwitchState(new RangeIdleState(stateMachine));
                    return;
                }
            }
        }
    }

    public override void Exit() {}
}
