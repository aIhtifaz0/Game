using UnityEngine;

public class EliteMeleeMainState : EliteMeleeBaseState{
    
   public EliteMeleeMainState(EliteMeleeStateMachine stateMachine) : base(stateMachine) {}

    private readonly int MovementHash = Animator.StringToHash("MovementBlendTree");
    private readonly int MovementSpeed = Animator.StringToHash("MovementSpeed");

    private const float CrossFadeDuration = 0.1f;
    private const float AnimatorDampTime = 0.1f;
    private float duration = 2f;
    private bool IsMovingRight;

    public override void Enter() {

        AudioManager.Instance.PlaySound(AudioManager.Instance.Walk);
        stateMachine.EdgeCheck.ChangeDirection += ChangeEnemyDirection;

        //setting to movement blendtree
        stateMachine.Animator.CrossFadeInFixedTime(MovementHash, CrossFadeDuration);
    }

    public override void Tick(float deltatime) {

        #region Collision Check
        if (Physics2D.OverlapBox(stateMachine._groundCheckPoint.position, stateMachine._groundCheckSize, 0, stateMachine._groundLayer)){ //checks if set box overlaps with ground >> && stateMachine.IsJumping bug IsGrounded cannot true
           
            stateMachine.IsGrounded = true;
        }
        #endregion

        if(stateMachine.AstralMode.IsAstral){
            
            CheckAstralDashing();
        }
        else if(!stateMachine.AstralMode.IsAstral){

            CheckPlayerDashing();
        }

        duration -= deltatime;

        if(duration <= 0f){

            stateMachine.SwitchState(new EliteMeleeTeleportState(stateMachine));
        }

        if(IsInAttackRange()){

            stateMachine.SwitchState(new EliteMeleeAttackState(stateMachine, 0));
        }

        MoveToPlayer(deltatime);

        CheckFacing();
        stateMachine.Animator.SetFloat(MovementSpeed, 0f, AnimatorDampTime, deltatime);
    }

    public override void Exit() {

        stateMachine.EdgeCheck.ChangeDirection -= ChangeEnemyDirection;
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
