using System.Collections;
using UnityEngine;

public class AstralDashState : AstralBaseState{
    
    private readonly int DashHash = Animator.StringToHash("Dash");

    private const float CrossFadeDuration = 0.1f;

    public AstralDashState(AstralStateMachine stateMachine) : base(stateMachine) {}

    public override void Enter() {

        stateMachine.Audio.PlaySound(stateMachine.Audio.Dash);
        stateMachine.Stats.SetInvulnerable(true);

        if(stateMachine.IsWall) { 

            stateMachine.StopAllCoroutines();
            stateMachine.SwitchState(new AstralMainState(stateMachine));
        }

        stateMachine.Animator.CrossFadeInFixedTime(DashHash, CrossFadeDuration);

        stateMachine.StartCoroutine(stateMachine.StartDash(stateMachine._lastDashDir));
    }

    public override void Tick(float deltatime) {}

    public override void Exit() {
        
        stateMachine.Stats.SetInvulnerable(false);
    }
}
/*
    getting animation from animator
    Play animation with crossfade for smooth transition
    
    StartDash : if dashing reset every ground check and decreasing dashleft
    setting no gravity and player move by dash direction + dash speed until dash time end
    resetting back the gravity and decrease the dash speed and back to main state
*/