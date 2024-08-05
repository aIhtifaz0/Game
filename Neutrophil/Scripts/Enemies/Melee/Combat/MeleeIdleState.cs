using UnityEngine;

public class MeleeIdleState : MeleeBaseState {

    public MeleeIdleState(MeleeStateMachine stateMachine) : base(stateMachine) {}

    private readonly int MovementHash = Animator.StringToHash("MovementBlendTree");
    private readonly int MovementSpeed = Animator.StringToHash("MovementSpeed");

    private const float CrossFadeDuration = 0.1f;
    private const float AnimatorDampTime = 0.1f;

    private float shootDuration;
    private float timeBetweenShoot = 1f;

    private float duration;

    public override void Enter() {

        duration = Random.Range(1f, 3f);
        shootDuration = timeBetweenShoot;
        //setting to movement blendtree
        stateMachine.Animator.CrossFadeInFixedTime(MovementHash, CrossFadeDuration);
    }

    public override void Tick(float deltatime) {

        Debug.Log(stateMachine.IsUltimate);
        stateMachine.Rigidbody.velocity = Vector2.zero;

        if (duration <= 0f && stateMachine.MeleeType != MeleeStateMachine.Melee.StayMelee && stateMachine.MeleeType != MeleeStateMachine.Melee.BossMelee)
        {
            stateMachine.SwitchState(new MeleePatrolState(stateMachine));
        }
        else
        {
            duration -= deltatime;
        }

        if (IsInSpotRange() && stateMachine.MeleeType != MeleeStateMachine.Melee.StayMelee && stateMachine.MeleeType != MeleeStateMachine.Melee.BossMelee) {

            stateMachine.SwitchState(new MeleeSpotState(stateMachine));
            return;
        }

        if (IsInAttackRange() && stateMachine.MeleeType == MeleeStateMachine.Melee.StayMelee) {

            stateMachine.SwitchState(new MeleeAttackState(stateMachine, 0));
            return;
        }

        if(!stateMachine.IsSpecialDone && IsInAttackRange()){
            
            stateMachine.SwitchState(new MeleeSpecialState(stateMachine));
            return;
        }
        

        shootDuration -= deltatime;

        if(shootDuration <= 0f){
            if(IsInSpotRange()){
                if(!stateMachine.IsShooting){
                    if(stateMachine.IsUltimate && stateMachine.Stats.health <=  30 * stateMachine.Stats.maxHealth / 100){
                        if (stateMachine.MeleeType == MeleeStateMachine.Melee.BossMelee) {
                            stateMachine.SwitchState(new MeleeUltimateState(stateMachine));
                            return;
                        }
                    }

                    if (stateMachine.MeleeType == MeleeStateMachine.Melee.BossMelee) {
                        stateMachine.IsShooting = true;
                        stateMachine.SwitchState(new MeleeAttackState(stateMachine, 0));
                    }
                }
            }
        }
        stateMachine.Animator.SetFloat(MovementSpeed, 0f, AnimatorDampTime, deltatime);
    }

    public override void Exit() {

    }
}