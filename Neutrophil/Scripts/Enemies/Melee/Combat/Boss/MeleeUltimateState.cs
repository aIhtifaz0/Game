using UnityEngine;

public class MeleeUltimateState : MeleeBaseState{

    public MeleeUltimateState(MeleeStateMachine stateMachine) : base(stateMachine) {}

    //Convert String to Hash for Animation
    private readonly int UltimateHash = Animator.StringToHash("Ultimate");
    private const float TransitionDuration = 0.1f;
    private float shootDuration;


    public override void Enter() {

        stateMachine.Animator.CrossFadeInFixedTime(UltimateHash, TransitionDuration);
    }

    public override void Tick(float deltatime) {

        shootDuration -= deltatime;
        Debug.Log("Ultimate");
    
        if(shootDuration <= 0f){

            ShootAttack(stateMachine.Summon);

            if(IsInSpotRange()){

                if(!stateMachine.IsShooting){
                    if (stateMachine.MeleeType == MeleeStateMachine.Melee.BossMelee) {
                        stateMachine.IsShooting = true;
                        stateMachine.SwitchState(new MeleeAttackState(stateMachine, 0));
                        return;
                    }
                }
            }
            else{
                stateMachine.SwitchState(new MeleeIdleState(stateMachine));
            }
        }
    }

    public override void Exit() {
        stateMachine.IsUltimate = false;
    }
}
