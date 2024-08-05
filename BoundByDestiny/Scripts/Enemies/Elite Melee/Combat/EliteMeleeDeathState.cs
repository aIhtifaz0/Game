using UnityEngine;

public class EliteMeleeDeathState : EliteMeleeBaseState {

    public EliteMeleeDeathState(EliteMeleeStateMachine stateMachine) : base(stateMachine) {}

    public override void Enter() {

        stateMachine.Rigidbody.velocity = Vector3.zero;
        
        //disable weapon collider
        stateMachine.Weapon.gameObject.SetActive(false);

        Object.Destroy(stateMachine.gameObject, 1f);
    }

    public override void Tick(float deltatime) {

        if(stateMachine.AstralMode.IsAstral){
            
            CheckAstralDashing();
        }
        else if(!stateMachine.AstralMode.IsAstral){

            CheckPlayerDashing();
        }
    }

    public override void Exit() {}
}
