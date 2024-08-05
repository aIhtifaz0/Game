using UnityEngine;
using System.Collections;

public class PlayerStateMachine : StateMachine{

    [field: Space(20)]
    [field: SerializeField] public InputReader InputReader { get; private set; }
    [field: SerializeField] public AstralStateMachine AstralStateMachine { get; private set; }
    [field: SerializeField] public WeaponDamagePlayer Weapon { get; private set; }
    [field: SerializeField] public ForceReceiver ForceReceiver { get; private set; }
    [field: SerializeField] public PlayerStats Stats { get; private set; }
    [field: SerializeField] public AstralModeManager Astral { get; private set; }
    [field: SerializeField] public AudioManager Audio { get; private set; }
    [field: Space(20)]
    [field: SerializeField] public Rigidbody2D Rigidbody { get; private set; }
    [field: SerializeField] public Animator Animator { get; private set; }
    [field: SerializeField] public CapsuleCollider2D Collider { get; private set; }
    [field: Space(20)]
    [field: SerializeField] public PlayerControllerData ControllerData { get; private set; }
    [field: SerializeField] public GameObject HitVfx {get; private set; }
    [field: SerializeField] public Finish Finish {get; private set; }
    [field: Space(20)]
    [field: SerializeField] public BasicAttackData[] Attack { get; private set; }
    [field: SerializeField] public BasicAttackData UpAttack { get; private set; }
    [field: SerializeField] public BasicAttackData AirAttack { get; private set; }
    [field: SerializeField] public BasicAttackData AirUpAttack { get; private set; }
    [field: SerializeField] public Transform[] TeleportPos  {get; private set; }


    public bool IsFacingRight { get; set; } //Player facing check
    public bool IsGrounded { get; set; } //Player On Ground check
    public bool IsJumping { get;  set; } //Player jumping check
    public bool IsDashing { get;  set; } //Player dashing check
    public bool IsWall { get;  set; } //Player dashing check
    public bool IsSlowMotion { get;  set; } 
    public float LastOnGroundTime { get;  set; } //Time player last on ground

    #region Jump Parameters
    [HideInInspector] public bool _isJumpFalling;
    #endregion

    #region Dash Parameters
    [HideInInspector] public int _dashesLeft; //Dash amount
    [HideInInspector] public bool _dashRefilling;
    [HideInInspector] public Vector2 _lastDashDir;
    [HideInInspector] public bool _isDashAttacking;
    #endregion

    #region Input Parameters
    [HideInInspector] public Vector2 _moveInput; //Move input var cache
    [HideInInspector] public float LastPressedJumpTime { get;  set; } //main parameter for jump input condition
    [HideInInspector] public float LastPressedDashTime { get;  set; } //main parameter for dash input condition
    [Space(15)]
    #endregion

    #region Check Parameters
    [Header("Checks")]
    public Transform _groundCheckPoint;
    public Vector2 _groundCheckSize = new Vector2(0.49f, 0.03f); //Ground check size
    [Space(15)]
    #endregion

    #region Layers & Tags
    [Header("Layers & Tags")]
    public LayerMask _groundLayer; //ground layer for land check
    #endregion


    private void Start(){

        IsFacingRight = true; //facing right at start

        //Calling the first state and get the Statemachine to the constructor 
        SwitchState(new PlayerMainState(this));
    }


    //call when script is active
    private void OnEnable(){

        Stats.OnTakeDamage += HandleTakeDamage;
        Stats.OnDie += HandleDie;
    }

    //call when script is unactive
    private void OnDisable(){

        Stats.OnTakeDamage -= HandleTakeDamage;
        Stats.OnDie -= HandleDie;
    }

    //call whenever it take damage and change to impact state
    private void HandleTakeDamage(){
        Audio.PlaySound(Audio.Hit);
        SwitchState(new PlayerImpactState(this));
    }

    //call whenever die and change to dead state
    private void HandleDie(){
        Audio.PlaySound(Audio.Death);
        SwitchState(new PlayerDeathState(this));
    }

    public void SlowMo(float duration){

        if(IsSlowMotion)
            return;
        Time.timeScale = 0.0f;
        StartCoroutine(SlowMotion(duration));
    }

    public IEnumerator SlowMotion(float duration){
        
        IsSlowMotion = true;
        yield return new WaitForSecondsRealtime(duration);
        Time.timeScale = 1.0f;
        IsSlowMotion = false;
    }

    //Short period before the player is able to dash again
    public IEnumerator RefillDash(int amount) {
        //SHoet cooldown, so we can't constantly dash along the ground, again this is the implementation in Celeste, feel free to change it up
        _dashRefilling = true;
        yield return new WaitForSeconds(ControllerData.DashRefillTime);
        _dashRefilling = false;
        _dashesLeft = Mathf.Min(ControllerData.dashAmount, _dashesLeft + 1);
    }

    public IEnumerator StartDash(Vector2 dir) {

        LastOnGroundTime = 0;
        LastPressedDashTime = 0;

        float startTime = Time.time;

        _dashesLeft--;
        _isDashAttacking = true;

        ForceReceiver.SetGravityScale(0);

        //We keep the player's velocity at the dash speed during the "attack" phase (in celeste the first 0.15s)
        while (Time.time - startTime <= ControllerData.DashAttackTime) {
            Rigidbody.velocity = dir.normalized * ControllerData.DashSpeed;
            //Pauses the loop until the next frame, creating something of a Update loop. 
            //This is a cleaner implementation opposed to multiple timers and this coroutine approach is actually what is used in Celeste :D
            yield return null;
        }

        startTime = Time.time;

        _isDashAttacking = false;

        //Begins the "end" of our dash where we return some control to the player but still limit run acceleration (see Update() and Run())
        ForceReceiver.SetGravityScale(ControllerData.GravityScale);
        Rigidbody.velocity = ControllerData.DashEndSpeed * dir.normalized;

        while (Time.time - startTime <= ControllerData.DashEndTime) {
            yield return null;
        }

        //Dash over
        IsDashing = false;
        SwitchState(new PlayerMainState(this));
    }

}
/*
    Storing every reference and public variable
    
    Call MainState at first state
    
    RefillDash : if dash refilling true call coroutine to refill dash after some time passed

    StartDash : if dashing reset every ground check and decreasing dashleft
    setting no gravity and player move by dash direction + dash speed until dash time end
    resetting back the gravity and decrease the dash speed and back to main state
*/