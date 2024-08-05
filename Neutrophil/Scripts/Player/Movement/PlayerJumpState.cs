using UnityEngine;

public class PlayerJumpState : PlayerBaseState{

    private readonly int JumpHash = Animator.StringToHash("Jump");

    private const float CrossFadeDuration = 0.1f;
    private float force;

    public PlayerJumpState(PlayerStateMachine stateMachine) : base(stateMachine) {}

    public override void Enter() {

        stateMachine.Audio.PlaySound(stateMachine.Audio.Jump);
        stateMachine.Animator.CrossFadeInFixedTime(JumpHash, CrossFadeDuration);
        stateMachine.LastOnGroundTime = stateMachine.ControllerData.coyoteTime;

        Jumping();
    }

    public override void Tick(float deltatime) {


        #region Input Check
        stateMachine._moveInput.x = stateMachine.InputReader.MovementValue.x;
        stateMachine._moveInput.y = stateMachine.InputReader.MovementValue.y;
        #endregion

        #region Attack Check
        if (stateMachine.InputReader.IsUltimate && stateMachine.Ultimate > 0) {

            stateMachine.Rigidbody.velocity = Vector3.zero;
            stateMachine.SwitchState(new PlayerMainUltimateAttackState(stateMachine));
            return;
        }
        if (stateMachine.InputReader.IsBasicAttacking && stateMachine.InputReader.MovementValue.y > 0f) {

            stateMachine.Rigidbody.velocity = Vector3.zero;
            stateMachine.SwitchState(new PlayerMainUpAttackState(stateMachine));
            return;
        }
        if (stateMachine.InputReader.IsBasicAttacking && stateMachine.InputReader.MovementValue.y < 0f) {

            stateMachine.Rigidbody.velocity = Vector3.zero;
            stateMachine.SwitchState(new PlayerMainDownAttackState(stateMachine));
            return;
        }
        if (stateMachine.InputReader.IsAirAttacking) {

            stateMachine.Rigidbody.velocity = Vector3.zero;
            stateMachine.SwitchState(new PlayerMainAirAttackState(stateMachine, 0));
            return;
        }
        if (stateMachine.InputReader.IsTertiaryAttacking) {

            stateMachine.Rigidbody.velocity = Vector3.zero;
            stateMachine.SwitchState(new PlayerTertiaryAttackState(stateMachine));
            return;
        }
        if (stateMachine.InputReader.IsSecondaryAttacking) {

            stateMachine.Rigidbody.velocity = Vector3.zero;
            stateMachine.SwitchState(new PlayerSecondaryAttackState(stateMachine, 0));
            return;
        }
        #endregion

        CheckFacing();
        Airborne();

        if (stateMachine.Rigidbody.velocity.y < 0) {

            stateMachine.SwitchState(new PlayerFallState(stateMachine));
            return;
        }
    }

    public override void Exit() {}

    #region Jump Methods
    private void Jumping() {

        stateMachine.LastPressedJumpTime = 0;
        stateMachine.LastOnGroundTime = 0;

        #region Perform Jump

        force = stateMachine.ControllerData.jumpForce;
        if (stateMachine.Rigidbody.velocity.y < 0)
            force -= stateMachine.Rigidbody.velocity.y;

        stateMachine.Rigidbody.AddForce(Vector2.up * force, ForceMode2D.Impulse);
        #endregion
    }
    #endregion

    #region Move Method
    private void Move(float lerpAmount) {

        float targetSpeed = stateMachine._moveInput.x * stateMachine.ControllerData.RunMaxSpeed;
        targetSpeed = Mathf.Lerp(stateMachine.Rigidbody.velocity.x, targetSpeed, lerpAmount);

        #region Calculate AccelRate
        float accelRate;

        if (stateMachine.LastOnGroundTime > 0)
            accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? stateMachine.ControllerData.runAccelAmount : stateMachine.ControllerData.runDeccelAmount;
        else
            accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? stateMachine.ControllerData.runAccelAmount * stateMachine.ControllerData.accelInAir : stateMachine.ControllerData.runDeccelAmount * stateMachine.ControllerData.DeccelInAir;
        #endregion

        #region Add Bonus Jump Apex Acceleration
        if ((stateMachine.IsJumping || stateMachine._isJumpFalling) && Mathf.Abs(stateMachine.Rigidbody.velocity.y) < stateMachine.ControllerData.JumpHangTimeThreshold) {
            accelRate *= stateMachine.ControllerData.JumpHangAccelerationMult;
            targetSpeed *= stateMachine.ControllerData.JumpHangMaxSpeedMult;
        }
        #endregion

        float speedDif = targetSpeed - stateMachine.Rigidbody.velocity.x;
        float movement = speedDif * accelRate;

        stateMachine.Rigidbody.AddForce(movement * Vector2.right, ForceMode2D.Force);
    }
    #endregion

    private void Airborne() {
        if (!stateMachine.IsDashing) {
            Move(1);
        }
        else if (stateMachine._isDashAttacking) {
            Move(stateMachine.ControllerData.dashEndRunLerp);
        }
    }
}
/*
     getting animation from animator
     Play animation with crossfade for smooth transition

     Reset lastongroundtime for player ready to jump after resetting
     getting input check for air moving
     getting input check for attack and if attack switch to any attack state based on input
     check player facing based on x input
     check player y velocity value for switch to falling
     Jump : Player move to up y direction that decrease from time pass, move player with addforce method
     Airborne : checking if player dashing or just moving with move method
     Move : Player move with addforce based on player speed and input, and added speed acceleration
     added jump/fall acceleration based on player y velocity
*/
