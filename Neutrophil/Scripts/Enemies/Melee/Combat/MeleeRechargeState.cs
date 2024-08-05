using UnityEngine;

public class MeleeRechargeState : MeleeBaseState {

    public MeleeRechargeState(MeleeStateMachine stateMachine) : base(stateMachine){}

    private float duration = 1.5f;
    private bool IsMovingRight;

    private readonly int MovementHash = Animator.StringToHash("MovementBlendTree");
    private readonly int MovementSpeed = Animator.StringToHash("MovementSpeed");

    private const float CrossFadeDuration = 0.1f;
    private const float AnimatorDampTime = 0.1f; 

    public override void Enter(){

        stateMachine.Rigidbody.velocity = Vector3.zero;

        stateMachine.EdgeCheck.ChangeDirection += ChangeEnemyDirection;

        //setting to movement blendtree
        stateMachine.Animator.CrossFadeInFixedTime(MovementHash, CrossFadeDuration);
    }

    public override void Tick(float deltatime){

        duration -= deltatime;

        if(duration <= 0f){
            stateMachine.SwitchState(new MeleeSpotState(stateMachine));
        }

        stateMachine.Animator.SetFloat(MovementSpeed, 0f, AnimatorDampTime, deltatime);
    }

    public override void Exit(){

        stateMachine.EdgeCheck.ChangeDirection -= ChangeEnemyDirection;
        stateMachine.IsRecharging = false;
    }

    public void ChangeEnemyDirection(){

        if(IsMovingRight){

            Turn();
        }
        else if(!IsMovingRight){

            Turn();
        }
    }
}
