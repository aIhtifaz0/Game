using UnityEngine;

public class RangePatrolState : RangeBaseState{

    public RangePatrolState(RangeStateMachine stateMachine) : base(stateMachine) {}

    //Convert String to Hash for Animation
    private readonly int MovementHash = Animator.StringToHash("MovementBlendTree");
    private readonly int MovementSpeed = Animator.StringToHash("MovementSpeed");

    private const float CrossFadeDuration = 0.1f;
    private const float AnimatorDampTime = 0.1f;

    private float duration;
    private float randomValue;

    private bool IsMovingRight;
    private Transform RandomPos;

    public override void Enter() {

        stateMachine.EdgeCheck.ChangeDirection += ChangeEnemyDirection;

        duration = Random.Range(3f, 8f);
        randomValue = Random.Range(-10f, 10f);

        if(randomValue > 0f && !IsMovingRight){

            RandomPos = stateMachine.RanPos[0];  
            IsMovingRight = true;
        }
        else if(randomValue < 0f && IsMovingRight){

            RandomPos = stateMachine.RanPos[1];
            IsMovingRight = false;  
        }

        //playing impact animation when it start
        stateMachine.Animator.CrossFadeInFixedTime(MovementHash, CrossFadeDuration);
    }

    public override void Tick(float deltatime) {

        if(stateMachine.AstralMode.IsAstral){
            
            CheckAstralDashing();
        }
        else if(!stateMachine.AstralMode.IsAstral){

            CheckPlayerDashing();
        }
        
        if (duration <= 0f){
            
            stateMachine.SwitchState(new RangeIdleState(stateMachine));
        }
        else{

            duration -= deltatime;

            if(RandomPos == null) {

                RandomPos = stateMachine.RanPos[Random.Range(0, 1)];
                RandomMove(RandomPos, deltatime);
            }
            RandomMove(RandomPos, deltatime);
        }

        if(IsInSpotRange()) {

            stateMachine.SwitchState(new RangeMainState(stateMachine));
        }

        stateMachine.Animator.SetFloat(MovementSpeed, 1f, AnimatorDampTime, deltatime);
    }

    public override void Exit() {

        stateMachine.EdgeCheck.ChangeDirection -= ChangeEnemyDirection;
    }

    public void ChangeEnemyDirection(){

        if(IsMovingRight){

            Turn();
        }
        else if(!IsMovingRight){

            Turn();
        }
    }

    protected void RandomMove(Transform newPos, float deltaTime) {
        
        stateMachine.transform.position = Vector2.MoveTowards(stateMachine.transform.position, newPos.position, stateMachine.PatrolSpeed * deltaTime);
    }
}
