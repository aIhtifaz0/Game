using UnityEngine;

public class PlayerMainState : PlayerBaseState{

    public PlayerMainState(PlayerStateMachine stateMachine, bool shouldFade = true) : base(stateMachine) {
        this.shouldFade = shouldFade;
    }

    private readonly int MovementHash = Animator.StringToHash("Movement");
    private readonly int SpeedHash = Animator.StringToHash("Speed");

    private bool shouldFade;

    private const float AnimatorDampTime = 0.1f;
    private const float CrossFadeDuration = 0.1f;

    public override void Enter(){

        stateMachine.LastPressedDashTime = 0f;
        stateMachine.LastPressedJumpTime = 0f;

        #region Subscribe InputEvent
        stateMachine.InputReader.JumpEvent += Jump;
        stateMachine.InputReader.DashEvent += Dash;
        stateMachine.InputReader.InteractEvent += Interact;
        stateMachine.InputReader.HealEvent += Heal;
        stateMachine.InputReader.MapEvent += Map;
        stateMachine.InputReader.PauseEvent += Pause;
        #endregion

        //stateMachine.Animator.SetFloat(MovementHash, 0);
        if (shouldFade) {
            stateMachine.Animator.CrossFadeInFixedTime(MovementHash, CrossFadeDuration);
        }
        else {
            stateMachine.Animator.Play(MovementHash);
        }
        stateMachine.ForceReceiver.SetGravityScale(stateMachine.ControllerData.GravityScale);
    }

    public override void Tick(float deltaTime) {

        #region Timers
        stateMachine.LastOnGroundTime -= deltaTime;
        stateMachine.LastPressedJumpTime -= deltaTime;
        stateMachine.LastPressedDashTime -= deltaTime;
        #endregion

        #region Calculate Input
        stateMachine._moveInput.x = stateMachine.InputReader.MovementValue.x;
        stateMachine._moveInput.y = stateMachine.InputReader.MovementValue.y;

        CheckFacing();
        #endregion

        #region Collision Check
        if (!stateMachine.IsDashing && !stateMachine.IsJumping) {
            //Ground Check
            if (Physics2D.OverlapBox(stateMachine._groundCheckPoint.position, stateMachine._groundCheckSize, 0, stateMachine._groundLayer) && !stateMachine.IsJumping) //checks if set box overlaps with ground
            {
                stateMachine.LastOnGroundTime = stateMachine.ControllerData.coyoteTime; //if so sets the lastGrounded to coyoteTime
                stateMachine.IsGrounded = true;
            }
        }
        #endregion

        #region Jump Checks
        if (stateMachine.IsJumping && stateMachine.Rigidbody.velocity.y < 0) { //falling check
            stateMachine.IsJumping = false;
        }

        if (!stateMachine.IsDashing) {
            //Jump
            if (CanJump() && stateMachine.LastPressedJumpTime > 0) {

                stateMachine.IsJumping = true;
                stateMachine._isJumpFalling = false;

                OnJump();
            }
        }
        #endregion

        #region Dash Checks

            if (CanDash() && stateMachine.LastPressedDashTime > 0 && stateMachine._lastDashDir.y == 0f) {

                    //If not direction pressed, dash forward
                    if (stateMachine._moveInput.x != 0f)
                        stateMachine._lastDashDir.x = stateMachine._moveInput.x;
                    else
                        stateMachine._lastDashDir = stateMachine.IsFacingRight ? Vector2.right : Vector2.left;

                    stateMachine.IsDashing = true;
                    stateMachine.IsJumping = false;

                    OnDash();           
        }
        #endregion

        #region Attack Check
        // if (stateMachine.InputReader.IsUltimate && stateMachine.Ultimate > 0) {

        //     stateMachine.Rigidbody.velocity = Vector3.zero;
        //     stateMachine.SwitchState(new PlayerUltimateState(stateMachine));
        //     return;
        // }
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

        if (stateMachine.InputReader.IsBasicAttacking) {

            stateMachine.Rigidbody.velocity = Vector3.zero;
            stateMachine.SwitchState(new PlayerMainBasicAttackState(stateMachine, 0));
            return;
        }

        if (stateMachine.InputReader.IsTertiaryAttacking && stateMachine.Stats.mana >= 80f) {

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

        if (stateMachine.Rigidbody.velocity.y < -1f) {
            stateMachine.IsGrounded = false;    
            stateMachine.SwitchState(new PlayerFallState(stateMachine));
        }

        //Move
        if (!stateMachine.IsDashing) {
            Run(1);
        }
        else if (stateMachine._isDashAttacking) {
            Run(stateMachine.ControllerData.dashEndRunLerp);
        }

        if (stateMachine.InputReader.MovementValue.x == 0f) {
            stateMachine.Animator.SetFloat(SpeedHash, 0, AnimatorDampTime, deltaTime);
            return;
        }
        stateMachine.Animator.SetFloat(SpeedHash, 1, AnimatorDampTime, deltaTime);
    }

    public override void Exit(){

        #region Unsubscribe InputEvent
        stateMachine.InputReader.JumpEvent -= Jump;
        stateMachine.InputReader.DashEvent -= Dash;
        stateMachine.InputReader.InteractEvent -= Interact;
        stateMachine.InputReader.HealEvent -= Heal;
        stateMachine.InputReader.MapEvent -= Map;
        stateMachine.InputReader.PauseEvent -= Pause;
        #endregion
    }

    #region Event Method
    private void Jump() {

        stateMachine.LastPressedJumpTime = stateMachine.ControllerData.jumpInputBufferTime;
    }

    private void Dash() {
        stateMachine.LastPressedDashTime = stateMachine.ControllerData.dashInputBufferTime;
    }

    private void Interact() {

    }

    private void Heal() {

        if(stateMachine.Stats.Heals > 0){
            stateMachine.SwitchState(new PlayerHealState(stateMachine));
        }
    }

    private void Map() {

    }

    private void Pause() {
        stateMachine.UI.PauseGame();
    }
    #endregion

    #region Run Method
    private void Run(float lerpAmount) {

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

    private void OnJump() {

        stateMachine.IsGrounded = false;
        stateMachine.SwitchState(new PlayerJumpState(stateMachine));
    }

    private void OnDash() {

        stateMachine.SwitchState(new PlayerDashState(stateMachine));
    }
}
/*
    getting animation blendtree and value
    getting all input event
    playing blendtree animation
    resetting every timer value and input checking
    check player facing based on x input

    checking player on ground, jumping, dashing and attack
    move player and playing move animation
    unsubscribing every event when switch state

    Run : Player move with addforce based on player speed and input, and added speed acceleration
*/
