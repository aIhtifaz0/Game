using UnityEngine;

public class FlyDeathState : FlyBaseState {

    public FlyDeathState(FlyStateMachine stateMachine) : base(stateMachine) {}


    public override void Enter() {

        stateMachine.Rigidbody.velocity = Vector3.zero;

        //disable weapon collider
        stateMachine.Weapon.gameObject.SetActive(false);

        Object.Destroy(stateMachine.gameObject, 0.3f);
    }

    public override void Tick(float deltatime) {

    }

    public override void Exit() {}
}
