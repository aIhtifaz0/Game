using UnityEngine;

public class MeleeSpotState : MeleeBaseState{
    
   public MeleeSpotState(MeleeStateMachine stateMachine) : base(stateMachine) {}

    private readonly int MovementHash = Animator.StringToHash("MovementBlendTree");
    private readonly int MovementSpeed = Animator.StringToHash("MovementSpeed");

    private const float CrossFadeDuration = 0.1f;
    private const float AnimatorDampTime = 0.1f;

    private float randomValue;
    private bool IsMovingRight;
    private float duration = 2.5f;

    public override void Enter() {

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

        if(!stateMachine.IsUltimate){

            if(stateMachine.MeleeType == MeleeStateMachine.Melee.BossMelee && stateMachine.Stats.health <=  (30 * stateMachine.Stats.maxHealth) / 100){

                stateMachine.AttackRange = 10.5f;
                stateMachine.IsUltimate = true;
                stateMachine.SwitchState(new MeleeUltimateState(stateMachine));
            }    
        }
    }

    public override void Tick(float deltatime) {


        #region Collision Check
        if (Physics2D.OverlapBox(stateMachine._groundCheckPoint.position, stateMachine._groundCheckSize, 0, stateMachine._groundLayer)){ //checks if set box overlaps with ground >> && stateMachine.IsJumping bug IsGrounded cannot true
           
            stateMachine.IsGrounded = true;
        }
        #endregion

        duration -= deltatime;

        if(duration <= 0){

            if(stateMachine.MeleeType == MeleeStateMachine.Melee.BossMelee && stateMachine.Stats.health <=  (60 * stateMachine.Stats.maxHealth) / 100){
                stateMachine.SwitchState(new MeleeSpecialState(stateMachine));
                duration = 2.5f;
            }
        }

        if(IsInAttackRange()){

            CheckFacing();

            if(stateMachine.MeleeType == MeleeStateMachine.Melee.NormalMelee){
                stateMachine.SwitchState(new MeleeAttackState(stateMachine, 0));
                return;
            }

            if(stateMachine.MeleeType == MeleeStateMachine.Melee.BossMelee && stateMachine.Stats.health >  (30 * stateMachine.Stats.maxHealth) / 100){
                stateMachine.SwitchState(new MeleeAttackState(stateMachine, 0));
                return;
            }

            if(stateMachine.MeleeType == MeleeStateMachine.Melee.BossMelee && stateMachine.Stats.health <=  (30 * stateMachine.Stats.maxHealth) / 100){
                stateMachine.SwitchState(new MeleeDashAttackState(stateMachine, 0));
                return;
            }
        }

        if(IsInJumpRange()){
            if(stateMachine.MeleeType == MeleeStateMachine.Melee.JumpMelee){
                stateMachine.SwitchState(new MeleeJumpAttackState(stateMachine, 0));
                return;
            }

            if(IsInAttackRange() && randomValue > 0f){
                if(stateMachine.MeleeType == MeleeStateMachine.Melee.StrongMelee) {
                    stateMachine.SwitchState(new MeleeAttackState(stateMachine, 0));
                }
            }
            else if(randomValue < 0f){
                if(stateMachine.MeleeType == MeleeStateMachine.Melee.StrongMelee) {
                    stateMachine.SwitchState(new MeleeJumpAttackState(stateMachine, 0));
                    return;
                }
            }
        }

        if (!IsInSpotRange()) {
            stateMachine.SwitchState(new MeleeIdleState(stateMachine));
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

            stateMachine.SwitchState(new MeleeIdleState(stateMachine));
        }

        if(stateMachine.Player.transform.position.x > stateMachine.transform.position.x + 5f || stateMachine.Player.transform.position.x < stateMachine.transform.position.x - 5f) { 

            stateMachine.SwitchState(new MeleeIdleState(stateMachine));
        }

        if(IsMovingRight){

            Turn();
        }
        else if(!IsMovingRight){

            Turn();
        }
    }
}
