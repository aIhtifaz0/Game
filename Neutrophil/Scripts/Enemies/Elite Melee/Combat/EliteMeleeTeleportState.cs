using System.Collections;
using UnityEngine;

public class EliteMeleeTeleportState : EliteMeleeBaseState{
    
    private readonly int TeleportHash = Animator.StringToHash("Teleport");

    private const float CrossFadeDuration = 0.1f;
    private float duration = 0.1f;
    private float randomValue;
    private bool IsMovingRight;
    private Transform RandomPos;

    public EliteMeleeTeleportState(EliteMeleeStateMachine stateMachine) : base(stateMachine) {}

    public override void Enter() {

        stateMachine.EdgeCheck.ChangeDirection += ChangeEnemyDirection;
        randomValue = Random.Range(-10f, 10f);

        if(randomValue > 0f && !IsMovingRight){

            RandomPos = stateMachine.RanPos[0];  
            IsMovingRight = true;
        }
        else if(randomValue < 0f && IsMovingRight){

            RandomPos = stateMachine.RanPos[1];
            IsMovingRight = false;  
        }

        RandomPos = stateMachine.RanPatrolPos[Random.Range(0, 1)];


        
        if(stateMachine.RanPos[0] != null && stateMachine.RanPos[1] != null ){
            RandomMove(RandomPos);
            return;
        }

        //stateMachine.Stats.SetInvulnerable(true);

        stateMachine.Animator.CrossFadeInFixedTime(TeleportHash, CrossFadeDuration);
    }

    public override void Tick(float deltatime) {

        duration -= deltatime;
        
        if(duration <= 0f){

            stateMachine.SwitchState(new EliteMeleeMainState(stateMachine));
        }
    }

    public override void Exit() {
        
        //stateMachine.Stats.SetInvulnerable(false);
        stateMachine.EdgeCheck.ChangeDirection -= ChangeEnemyDirection;
    }

    protected void RandomMove(Transform newPos) {
        
        stateMachine.transform.position = newPos.position;
    }

    public void ChangeEnemyDirection(){

        if(stateMachine.Player.transform.position.y > stateMachine.transform.position.y + 5f || stateMachine.Player.transform.position.y < stateMachine.transform.position.y - 5f) { 

            stateMachine.SwitchState(new EliteMeleeIdleState(stateMachine));
        }

        if(stateMachine.Player.transform.position.x > stateMachine.transform.position.x + 5f || stateMachine.Player.transform.position.x < stateMachine.transform.position.x - 5f) { 

            stateMachine.SwitchState(new EliteMeleeIdleState(stateMachine));
        }

        if(IsMovingRight){

            Turn();
        }
        else if(!IsMovingRight){

            Turn();
        }
    }
}
/*
    getting animation from animator
    Play animation with crossfade for smooth transition
    
    StartDash : if dashing reset every ground check and decreasing dashleft
    setting no gravity and player move by dash direction + dash speed until dash time end
    resetting back the gravity and decrease the dash speed and back to main state
*/