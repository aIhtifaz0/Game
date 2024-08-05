using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyShootState : FlyBaseState{

    public FlyShootState(FlyStateMachine stateMachine) : base(stateMachine) {}

    //private readonly int ShootHash = Animator.StringToHash("Shoot");
    private const float CrossFadeDuration = 0.1f;

    public override void Enter() {

        stateMachine.Audio.PlaySound(stateMachine.Audio.Shoot);
        stateMachine.Rigidbody.velocity = Vector3.zero;

        //stateMachine.Animator.CrossFadeInFixedTime(ShootHash, CrossFadeDuration);
        ShootAttack(stateMachine.Bullet);

        Vector2 lookDir = stateMachine.astralStateMachine.transform.position - stateMachine.ShotPoint.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        Quaternion rotate = Quaternion.AngleAxis(angle, Vector3.forward);
        stateMachine.ShotPoint.rotation = rotate;
        GameObject MuzzleFlash = Object.Instantiate(stateMachine.MuzzleVfx, stateMachine.ShotPoint.position, stateMachine.ShotPoint.rotation);
    }

    public override void Tick(float deltaTime) {

        if (IsInSpotRange()) {

            stateMachine.SwitchState(new FlyChaseState(stateMachine));
            return;
        }
    }

    public override void Exit() {

        stateMachine.IsAttacking = false;
    }
}
