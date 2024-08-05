using System.Diagnostics;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class RangeMainState : RangeBaseState{

   public RangeMainState(RangeStateMachine stateMachine) : base(stateMachine) {}

    readonly int MovementHash = Animator.StringToHash("MovementBlendTree");
    private readonly int MovementSpeed = Animator.StringToHash("MovementSpeed");

    private const float CrossFadeDuration = 0.1f;
    private const float AnimatorDampTime = 0.1f;

    private float AttackTimer;
    private float timerBetweenAttack = 1.3f;
    private float randomValue;
    private bool IsMovingRight;

    public override void Enter() {

        #region Collision Check
        if (Physics2D.OverlapBox(stateMachine._groundCheckPoint.position, stateMachine._groundCheckSize, 0, stateMachine._groundLayer)){ //checks if set box overlaps with ground >> && stateMachine.IsJumping bug IsGrounded cannot true
           
            stateMachine.IsGrounded = true;
        }
        #endregion

        AttackTimer = timerBetweenAttack;

        stateMachine.EdgeCheck.ChangeDirection += ChangeEnemyDirection;

        randomValue = Random.Range(-10f, 10f);

        if(randomValue > 0f && !IsMovingRight){

            IsMovingRight = true;
        }
        else if(randomValue < 0f && IsMovingRight){

            IsMovingRight = false;  
        }

        //setting to movement blendtree
        stateMachine.Animator.CrossFadeInFixedTime(MovementHash, CrossFadeDuration);
    }

    public override void Tick(float deltatime) {

        CheckPlayerDashing();

        if(AttackTimer <= 0f){

            stateMachine.IsAttacking = true;
            stateMachine.IsShooting = true;
            stateMachine.IsChargeAttacking = true;
        }
        else{
            AttackTimer -= deltatime;
        }

        if (IsInShootRange()) {

            if(stateMachine.IsShooting){
                stateMachine.SwitchState(new RangeShootState(stateMachine));
                return;
            }
        }

        UnityEngine.Debug.Log("Main");
        if(IsInAttackRange()){

            if(stateMachine.IsAttacking && stateMachine.RangeType == RangeStateMachine.Range.MeleeRange){
                stateMachine.SwitchState(new RangeAttackState(stateMachine, 0));
            }
            if(stateMachine.IsChargeAttacking && stateMachine.RangeType == RangeStateMachine.Range.EliteRange){
                stateMachine.SwitchState(new RangeAttackState(stateMachine, 0));
            }
        }

        if (!IsInSpotRange()) {
            stateMachine.SwitchState(new RangeIdleState(stateMachine));
            return;
        }

        CheckFacing();
        MoveToPlayer(deltatime);
        
        stateMachine.Animator.SetFloat(MovementSpeed, 1f, AnimatorDampTime, deltatime);
    }

    public override void Exit() {

        stateMachine.EdgeCheck.ChangeDirection -= ChangeEnemyDirection;
    }

    public void ChangeEnemyDirection(){

        if(stateMachine.Player.transform.position.y > stateMachine.transform.position.y + 5f || stateMachine.Player.transform.position.y < stateMachine.transform.position.y - 5f) { 

            stateMachine.SwitchState(new RangeIdleState(stateMachine));
        }

        if(stateMachine.Player.transform.position.x > stateMachine.transform.position.x + 5f || stateMachine.Player.transform.position.x < stateMachine.transform.position.x - 5f) { 

            stateMachine.SwitchState(new RangeIdleState(stateMachine));
        }

        if(IsMovingRight){

            Turn();
        }
        else if(!IsMovingRight){

            Turn();
        }
    }
}
