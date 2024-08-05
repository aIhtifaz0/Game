using UnityEngine;

[CreateAssetMenu(menuName = "Player Controller Data")]
public class PlayerControllerData : ScriptableObject {

    #region Gravity
    [field: Header("Physics")]
    [field: SerializeField] public float GravityScale { get; set; } //Rigidbody or Physics2D gravity of player 
    [field: SerializeField] public float GravityStrength { get; set; } //Downwards force for desired jumpHeight

    [field: Space(8)]
    [field: SerializeField] public float FallGravityMult { get; set; } //Multiplier gravity when player fall
    [field: SerializeField] public float MaxFallSpeed { get; set; } //Maximum fall speed player velocity
    [field: SerializeField] public float FastFallGravityMult { get; set; } //Multiplier gravity when player fall and input pressed
    [field: SerializeField] public float MaxFastFallSpeed { get; set; } //Maximum fall speed(terminal velocity) of the player when performing a faster fall.

    [field: Space(15)]
    #endregion

    #region Run
    [field: Space(15)]
    [field: Header("Run")]
    [field: SerializeField] public float RunMaxSpeed { get; set; } //Player run Target speed
    [field: SerializeField] public float RunAcceleration { get; set; } //Acceleration value to player maxSpeed for smoothness
    [HideInInspector] public float runAccelAmount; //Force value
    [field: SerializeField] public float RunDecceleration { get; set; } //Decceleration value to player maxSpeed for smoothness
    [HideInInspector] public float runDeccelAmount; //Force value
    [field: Space(8)]
    [Range(0f, 1f)] public float accelInAir; //Acceleration value when airborne
    [Range(0f, 1f)] public float DeccelInAir; //Decceleration value when airborne
    #endregion

    #region Jump
    [field: Space(15)]
    [field: Header("Jump")]
    [field: SerializeField] public float JumpHeight { get; set; } //Player jump Height
    [field: SerializeField] public float JumpTimeToApex { get; set; } //Time beetween applying jump force and reaching desired jump height
    [HideInInspector] public float jumpForce; //The actual jump force
    [field: Space(8)]
    [Range(0f, 1)] public float jumpHangGravityMult; //Reduce gravity when close to the desired jump height
    [field: SerializeField] public float JumpHangTimeThreshold { get; set; }
    [field: Space(0.5f)]
    [field: SerializeField] public float JumpHangAccelerationMult { get; set; } //Acceleration gravity reduce value then close to desired jump height
    [field: SerializeField] public float JumpHangMaxSpeedMult { get; set; } //Max speed for jump hang acceleration
    #endregion

    #region Assist
    [field: Space(15)]
    [Range(0.01f, 0.5f)] public float coyoteTime; //Time when player can still jumping after fall from platform
    [Range(0.01f, 0.5f)] public float jumpInputBufferTime; //Time where jump will automatically performed after requirement are met
    #endregion

    #region Dash
    [field: Space(15)]
    [field: Header("Dash")]
    public int dashAmount; //Dash amount for player
    [field: SerializeField] public float DashSpeed { get; set; } //Dash speed value
    [field: Space(8)]
    [field: SerializeField] public float DashSleepTime { get; set; } //Time when player can only read input and applying force
    [field: SerializeField] public float DashAttackTime { get; set; }
    [field: SerializeField] public float DashEndTime { get; set; } //Time after you finish the inital drag phase, smoothing the transition back to idle (or any standard state)
    [field: Space(8)]
    [field: SerializeField] public Vector2 DashEndSpeed { get; set; } //Slows down player after dash
    [field: Space(8)]
    [Range(0f, 1f)] public float dashEndRunLerp; //Slows the affect of player movement while dashing
    [field: Space(8)]
    [field: SerializeField] public float DashRefillTime { get; set; } //Dash refill time when dash end
    [Space(8)]
    [Range(0.01f, 0.5f)] public float dashInputBufferTime;
    #endregion

    private void OnValidate() {
        //Calculate downwards force after reach desired jump height by jumpheight divided by jump time when reach desired jump height
        GravityStrength = -(2 * JumpHeight) / (JumpTimeToApex * JumpTimeToApex);

        //Calculate garivity scale based on downward force after jump divided Physics2D gravity 
        GravityScale = GravityStrength / Physics2D.gravity.y;

        //Calculate run acceleration & deceleration forces 
        runAccelAmount = (50 * RunAcceleration) / RunMaxSpeed;
        runDeccelAmount = (50 * RunDecceleration) / RunMaxSpeed;

        //Calculate jumpForce based on gravity * time desired jump height
        jumpForce = Mathf.Abs(GravityStrength) * JumpTimeToApex;

        #region Variable Ranges
        //Callculate Run acceleratiion and decceleration value based run max speed
        RunAcceleration = Mathf.Clamp(RunAcceleration, 0.01f, RunMaxSpeed);
        RunDecceleration = Mathf.Clamp(RunDecceleration, 0.01f, RunMaxSpeed);
        #endregion
    }
}
