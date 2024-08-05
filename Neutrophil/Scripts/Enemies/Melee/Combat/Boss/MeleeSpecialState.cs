using UnityEngine;

public class MeleeSpecialState : MeleeBaseState{

    public MeleeSpecialState(MeleeStateMachine stateMachine) : base(stateMachine) {}

    //Convert String to Hash for Animation
    private readonly int SkillHash = Animator.StringToHash("Ball");
    private const float TransitionDuration = 0.1f;
    private float duration;
    private float randomValue;

    private bool IsMovingRight;
    private Transform RandomPos;

    public override void Enter() {

        stateMachine.Stats.SetInvulnerable(true);

        stateMachine.EdgeCheck.ChangeDirection += ChangeEnemyDirection;

        duration = 10f;
        randomValue = Random.Range(-10f, 10f);

        if(randomValue > 0f && !IsMovingRight){

            RandomPos = stateMachine.RanPos[0];  
            IsMovingRight = true;
        }
        else if(randomValue < 0f && IsMovingRight){

            RandomPos = stateMachine.RanPos[1];
            IsMovingRight = false;  
        }

        stateMachine.Weapon.SetAttack(20, 40f, 0f);

        //playing impact animation when it start
        stateMachine.Animator.CrossFadeInFixedTime(SkillHash, TransitionDuration);
    }

    public override void Tick(float deltatime) {

        if (duration <= 0f){
            
            stateMachine.SwitchState(new MeleeRevertState(stateMachine));
        }
        else{

            duration -= deltatime;

            if(RandomPos == null) {

                RandomPos = stateMachine.RanPos[Random.Range(0, 1)];
                RandomMove(RandomPos, deltatime);
            }
            RandomMove(RandomPos, deltatime);
        }
    }

    public override void Exit() {

        stateMachine.Stats.SetInvulnerable(false);

        stateMachine.EdgeCheck.ChangeDirection -= ChangeEnemyDirection;
        stateMachine.IsSpecialDone = true;
        stateMachine.IsUltimate = true;
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
        
        stateMachine.transform.position = Vector2.MoveTowards(stateMachine.transform.position, newPos.position, stateMachine.MovementSpeed * deltaTime);
    }
}
