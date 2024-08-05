    using UnityEngine;

public class PlayerFallState : PlayerBaseState
{

    private readonly int FallHash = Animator.StringToHash("Fall");

    private const float CrossFadeDuration = 0.1f;

    public PlayerFallState(PlayerStateMachine stateMachine) : base(stateMachine) {}

    public override void Enter() {

        stateMachine.Animator.CrossFadeInFixedTime(FallHash, CrossFadeDuration);
        stateMachine.LastOnGroundTime = stateMachine.ControllerData.coyoteTime;
    }

    public override void Tick(float deltatime) {

        #region Input Check
        stateMachine._moveInput.x = stateMachine.InputReader.MovementValue.x;
        stateMachine._moveInput.y = stateMachine.InputReader.MovementValue.y;
        #endregion

        #region Collision Check
        if (Physics2D.OverlapBox(stateMachine._groundCheckPoint.position, stateMachine._groundCheckSize, 0, stateMachine._groundLayer)) //checks if set box overlaps with ground >> && stateMachine.IsJumping bug IsGrounded cannot true
           {
            stateMachine.LastOnGroundTime = stateMachine.ControllerData.coyoteTime; //if so sets the lastGrounded to coyoteTime
            stateMachine.IsGrounded = true;
        }
        #endregion

        #region Attack Check
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
        if (stateMachine.InputReader.IsUltimate) {

            if (stateMachine.InputReader.IsBasicAttacking) {

                stateMachine.Rigidbody.velocity = Vector3.zero;
                stateMachine.SwitchState(new PlayerMainUltimateAttackState(stateMachine));
            }
            return;
        }
        #endregion

        CheckFacing();
        Airborne();

        if (stateMachine.IsGrounded) { // || !stateMachine.IsJumping BasicAttack after air attack bug

            stateMachine.SwitchState(new PlayerMainState(stateMachine));
        }
    }

    public override void Exit() {

        stateMachine.IsJumping = false;
    }
    private void Airborne() {
        if (!stateMachine.IsDashing) {
            Move(1);
        }
        else if (stateMachine._isDashAttacking) {
            Move(stateMachine.ControllerData.dashEndRunLerp);
        }
    }

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
}

/*
     getting animation from animator
     Play animation with crossfade for smooth transition

     Reset lastongroundtime for player ready to jump after resetting
     getting input check for air moving
     getting input check for attack and if attack switch to any attack state based on input
     check player facing based on x input
     Airborne : checking if player dashing or just moving with move method
     Move : Player move with addforce based on player speed and input, and added speed acceleration
     added jump/fall acceleration based on player y velocity
*/
