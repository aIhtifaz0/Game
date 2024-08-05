using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeTransformState : MeleeBaseState{
    public MeleeTransformState(MeleeStateMachine stateMachine) : base(stateMachine){}

    private readonly int TransformHash = Animator.StringToHash("Special");
    private const float TransitionDuration = 0.1f;
    private float duration = 1f;

    public override void Enter()
    {
        stateMachine.Stats.SetInvulnerable(true);

        stateMachine.Animator.CrossFadeInFixedTime(TransformHash, TransitionDuration);
    }

    public override void Tick(float deltatime)
    {
        duration -= deltatime;
        
        if (duration <= 0f) {

            stateMachine.SwitchState(new MeleeSpecialState(stateMachine));
        }
    }

    public override void Exit()
    {
        stateMachine.Stats.SetInvulnerable(false);
    }
}
