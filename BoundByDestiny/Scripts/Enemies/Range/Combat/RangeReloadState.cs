using UnityEngine;

public class RangeReloadState : RangeBaseState {

    public RangeReloadState(RangeStateMachine stateMachine) : base(stateMachine){}

    private float duration = 3;
    private float randomValue;
    private bool IsMovingRight;
    private Transform RandomPos;

    private readonly int MovementHash = Animator.StringToHash("MovementBlendTree");
    private readonly int ReloadHash = Animator.StringToHash("Reload");
    private readonly int MovementSpeed = Animator.StringToHash("MovementSpeed");

    private const float CrossFadeDuration = 0.1f;
    private const float AnimatorDampTime = 0.1f; 

    public override void Enter(){

        stateMachine.EdgeCheck.ChangeDirection += ChangeEnemyDirection;

        randomValue = Random.Range(-10f, 10f);

        if(stateMachine.RangeType == RangeStateMachine.Range.EliteRange && stateMachine.StrongBullet <= 0){
            stateMachine.StrongBullet = 2;
        }

        if(randomValue > 0f && !IsMovingRight){

            RandomPos = stateMachine.RanPos[0];  
            IsMovingRight = true;
        }
        else if(randomValue < 0f && IsMovingRight){

            RandomPos = stateMachine.RanPos[1];
            IsMovingRight = false;  
        }

        if(stateMachine.RangeType == RangeStateMachine.Range.NormalRange){
            stateMachine.Animator.CrossFadeInFixedTime(MovementHash, CrossFadeDuration);
        }

        if(stateMachine.RangeType == RangeStateMachine.Range.EliteRange){
            stateMachine.Animator.CrossFadeInFixedTime(ReloadHash, CrossFadeDuration);
        }
    }

    public override void Tick(float deltatime){

        duration -= deltatime;

        if(stateMachine.AstralMode.IsAstral){
            
            CheckAstralDashing();
        }
        else if(!stateMachine.AstralMode.IsAstral){

            CheckPlayerDashing();
        }

        if(IsInAttackRange()){

            if(stateMachine.IsChargeAttacking && stateMachine.RangeType == RangeStateMachine.Range.EliteRange){
                stateMachine.SwitchState(new RangeAttackState(stateMachine, 0));
            }
        }

        if(duration <= 0f){

            stateMachine.SwitchState(new RangeMainState(stateMachine));
        }
        else{

             if(stateMachine.RangeType == RangeStateMachine.Range.NormalRange){

                if(RandomPos == null) {

                    RandomPos = stateMachine.RanPos[Random.Range(0, 1)];
                    RandomMove(RandomPos, deltatime);
                }
                RandomMove(RandomPos, deltatime);
            }
        }

        if(stateMachine.RangeType == RangeStateMachine.Range.NormalRange){
            stateMachine.Animator.SetFloat(MovementSpeed, 1f, AnimatorDampTime, deltatime);
        }
    }

    public override void Exit(){

        stateMachine.EdgeCheck.ChangeDirection -= ChangeEnemyDirection;
        stateMachine.IsReloading = false;
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
