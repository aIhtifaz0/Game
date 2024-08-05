using UnityEngine;

public class MeleeDeathState : MeleeBaseState {

    public MeleeDeathState(MeleeStateMachine stateMachine) : base(stateMachine) {}

    public override void Enter() {

        stateMachine.Rigidbody.velocity = Vector3.zero;

        //disable weapon collider
        stateMachine.Weapon.gameObject.SetActive(false);

        Object.Destroy(stateMachine.gameObject, 0.2f);
    }

    public override void Tick(float deltatime) {

        CheckPlayerDashing();
    }

    public override void Exit() {}
}
