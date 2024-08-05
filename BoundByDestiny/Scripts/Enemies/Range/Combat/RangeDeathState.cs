using UnityEngine;

public class RangeDeathState : RangeBaseState {

    public RangeDeathState(RangeStateMachine stateMachine) : base(stateMachine) {}

    public override void Enter() {

        stateMachine.Rigidbody.velocity = Vector3.zero;
        
        // //disable weapon collider
        // stateMachine.Weapon.gameObject.SetActive(false);

        Object.Destroy(stateMachine.gameObject, 0.2f);
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
