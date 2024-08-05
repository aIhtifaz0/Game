using UnityEngine;

public class FlyChaseState : FlyBaseState {

    public FlyChaseState(FlyStateMachine stateMachine) : base(stateMachine) {}

    private readonly int FlightMovement = Animator.StringToHash("MovementBlendTree");
    private readonly int Speed = Animator.StringToHash("MovementSpeed");

    private const float CrossFadeDuration = 0.1f;
    private const float AnimatorDampTime = 0.1f;

    private float AttackTimer;
    private float timerBetweenAttack = 2f;
    private float randomValue;
    private bool IsMovingRight;


    public override void Enter() {

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
        stateMachine.Animator.CrossFadeInFixedTime(FlightMovement, CrossFadeDuration);
    }

    public override void Tick(float deltatime) {

        #region Collision Check
        if (Physics2D.OverlapBox(stateMachine._groundCheckPoint.position, stateMachine._groundCheckSize, 0, stateMachine._groundLayer)){ //checks if set box overlaps with ground >> && stateMachine.IsJumping bug IsGrounded cannot true
           
            stateMachine.IsGrounded = true;
        }
        #endregion

        if(AttackTimer <= 0f){

            stateMachine.IsAttacking = true;
        }
        else{

            AttackTimer -= deltatime;
        }

        if (IsInAttackRange() && stateMachine.FlyType == FlyStateMachine.Fly.NormalFly) {

            if (stateMachine.IsAttacking)
                stateMachine.SwitchState(new FlyAttackState(stateMachine, 0));
                return;
        }

        if (IsInShootRange() && stateMachine.FlyType == FlyStateMachine.Fly.RangeFly){

            if (stateMachine.IsAttacking) 
                stateMachine.SwitchState(new FlyShootState(stateMachine));
                return;        
        }

        if (!IsInSpotRange()) {

            stateMachine.SwitchState(new FlyFlightState(stateMachine));
        }

        CheckFacing();
        MoveToPlayer(deltatime);
        stateMachine.Animator.SetFloat(Speed, 1f, AnimatorDampTime, deltatime);
    }

    public override void Exit() {

        stateMachine.EdgeCheck.ChangeDirection -= ChangeEnemyDirection;
    }

     public void ChangeEnemyDirection(){

        if(stateMachine.Player.transform.position.y > stateMachine.transform.position.y + 10f || stateMachine.Player.transform.position.y < stateMachine.transform.position.y - 10f) { 

            stateMachine.SwitchState(new FlyFlightState(stateMachine));
        }

        if(stateMachine.Player.transform.position.x > stateMachine.transform.position.x + 10f || stateMachine.Player.transform.position.x < stateMachine.transform.position.x - 10f) { 

            stateMachine.SwitchState(new FlyFlightState(stateMachine));
        }

        if(IsMovingRight){

            Turn();
        }
        else if(!IsMovingRight){

            Turn();
        }
    }
}