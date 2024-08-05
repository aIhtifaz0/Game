using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class RangeShootState : RangeBaseState{

    public RangeShootState(RangeStateMachine stateMachine) : base(stateMachine) {}

    private readonly int ShootHash = Animator.StringToHash("Shoot");
    private const float CrossFadeDuration = 0.1f;
    private float duration = 2f;

    public override void Enter() {

        if(stateMachine.RangeType == RangeStateMachine.Range.EliteRange){
            stateMachine.StrongBullet--;
        }

        stateMachine.Rigidbody.velocity = Vector3.zero;

        stateMachine.Animator.CrossFadeInFixedTime(ShootHash, CrossFadeDuration);
        stateMachine.Audio.PlaySound(stateMachine.Audio.Shoot);
        
    }

    public override void Tick(float deltaTime) {

        CheckFacing();

        duration -= Time.deltaTime;

        CheckPlayerDashing();

        float normalizedTime = GetNormalizedTime(stateMachine.Animator, "Attack");

        if (normalizedTime > 0.9f) {

            
        }

        if(duration <= 0f){

            ShootAttack(stateMachine.Bullet);

            // Vector2 lookDir = stateMachine.Player.transform.position - stateMachine.ShotPoint.position;
            // float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
            // Quaternion rotate = Quaternion.AngleAxis(angle, Vector3.forward);
            // stateMachine.ShotPoint.rotation = rotate;
        
            if(stateMachine.MuzzleVfx != null){
                GameObject MuzzleFlash = Object.Instantiate(stateMachine.MuzzleVfx, stateMachine.ShotPoint.position, stateMachine.ShotPoint.rotation);
            }

            if(stateMachine.StrongBullet > 0 && stateMachine.RangeType == RangeStateMachine.Range.EliteRange){
                stateMachine.SwitchState(new RangeShootState(stateMachine));
                return;
            }

            if(stateMachine.RangeType == RangeStateMachine.Range.NormalRange || stateMachine.StrongBullet <= 0 && stateMachine.RangeType == RangeStateMachine.Range.EliteRange){
                stateMachine.SwitchState(new RangeReloadState(stateMachine));
                return;
            }

            if(stateMachine.RangeType == RangeStateMachine.Range.MeleeRange){
                stateMachine.SwitchState(new RangeMainState(stateMachine));
            }

            if(stateMachine.RangeType == RangeStateMachine.Range.StayRange){
                stateMachine.SwitchState(new RangeIdleState(stateMachine));
            }
        }
    }

    public override void Exit() {

        stateMachine.IsShooting = false;

        if(stateMachine.RangeType == RangeStateMachine.Range.NormalRange)
            stateMachine.IsReloading = true;
    }
}
