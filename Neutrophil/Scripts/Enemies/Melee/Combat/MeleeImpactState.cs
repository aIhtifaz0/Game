using UnityEngine;

public class MeleeImpactState : MeleeBaseState{

    public MeleeImpactState(MeleeStateMachine stateMachine) : base(stateMachine) {}

    //Convert String to Hash for Animation
    private readonly int ImpactHash = Animator.StringToHash("Impact");
    private const float TransitionDuration = 0.1f;
    private float duration;

    public override void Enter() {

        duration = stateMachine.StunDuration;

        //playing impact animation when it start
        if(stateMachine.MeleeType != MeleeStateMachine.Melee.BossMelee)
            stateMachine.Animator.CrossFadeInFixedTime(ImpactHash, TransitionDuration);
    }

    public override void Tick(float deltatime) {

        Debug.Log("Impact");
        //After hit duration pass back to previous state
        duration -= deltatime;
        
       if (duration <= 0f) {

            if (stateMachine.IsGrounded) {
                if(stateMachine.MeleeType != MeleeStateMachine.Melee.BossMelee) {

                    stateMachine.SwitchState(new MeleeSpotState(stateMachine));
                    return;
                }
                
            }

            if(stateMachine.MeleeType == MeleeStateMachine.Melee.BossMelee && !stateMachine.IsSpecialDone){
                    stateMachine.SwitchState(new MeleeIdleState(stateMachine));
            }
            if(stateMachine.MeleeType == MeleeStateMachine.Melee.BossMelee && stateMachine.IsSpecialDone){
                    stateMachine.SwitchState(new MeleeIdleState(stateMachine));
            }
        }
    }

    public override void Exit() {}
}
