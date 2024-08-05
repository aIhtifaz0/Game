using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyStateMachine : StateMachine {

    public enum Fly {
        NormalFly, RangeFly
    }
    
    [field: Space(20)]
    [field: SerializeField] public Fly FlyType { get; private set; }
    //[field: SerializeField] public Animator Animator { get; private set; }
    [field: SerializeField] public Rigidbody2D Rigidbody { get; private set; }
    [field: SerializeField] public CapsuleCollider2D Collider { get; private set; }
    [field: Space(20)]
    [field: SerializeField] public FlyForceReceiver ForceReceiver { get; private set; }
    [field: SerializeField] public AstralModeManager AstralMode { get; private set; }
    [field: SerializeField] public AudioManager Audio { get; private set; }
    [field: SerializeField] public WeaponDamageEnemy Weapon { get; private set; }
    [field: SerializeField] public WeaponHandler WeaponCollider { get; private set; }
    [field: SerializeField] public EnemyEdgeCheck EdgeCheck { get; private set; }
    [field: SerializeField] public GameObject HitVfx {get; private set; }
    [field: SerializeField] public EnemyStats Stats { get; private set; }
    [field: SerializeField] public PlayerControllerData ControllerData { get; private set; }
    [field: SerializeField] public GameObject Head {get; private set; }
    [field: Space(20)]
    [field: SerializeField] public float SpotRange { get; private set; }
    [field: SerializeField] public float AttackRange { get; private set; }
    [field: SerializeField] public float ShootRange { get; private set; }
    [field: SerializeField] public float AttackSpeed { get; private set; }
    [field: SerializeField] public float MovementSpeed { get; private set; }
    [field: SerializeField] public float PatrolSpeed { get; private set; }
    [field: Space(20)]
    [field: SerializeField] public Transform ShotPoint { get; private set; }
    [field: SerializeField] public GameObject Bullet { get; private set; }
    [field: SerializeField] public GameObject MuzzleVfx {get; private set; }
    [field: SerializeField] public Transform[] RanPos { get; private set; }
    [field: SerializeField] public FlyAttack Attacks { get; private set; }
    [field: Space(20)]

    public PlayerStats Player { get; private set; }
    public PlayerStateMachine playerStateMachine { get; private set; }
    public AstralStateMachine astralStateMachine { get; private set; }
    [field: Space(20)]

    public bool IsGrounded { get; set; }
    public bool IsFacingRight { get; set; } //Player facing check
    public bool IsDashing { get;  set; } //Player dashing checkwwwwwww
    public bool IsAttacking { get; set; }
    public bool IsSlowMotion { get;  set; } 

    #region Check Parameters
    [Header("Checks")]
    public Transform _groundCheckPoint;
    public Vector2 _groundCheckSize = new Vector2(0.49f, 0.03f); //Ground check size
    [field: Space(10)]
    #endregion

    #region Layers & Tags
    [Header("Layers & Tags")]
    public LayerMask _groundLayer; //ground layer for land check
    #endregion

    private void Awake() {

        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
        astralStateMachine = GameObject.FindGameObjectWithTag("Player").GetComponent<AstralStateMachine>();
        playerStateMachine = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStateMachine>();
    }

    private void Start(){
        
        SwitchState(new FlyFlightState(this));
    }

    //call when script is active
    private void OnEnable() {

        Stats.OnTakeDamage += HandleTakeDamage;
        Stats.OnDie += HandleDie;
    }

    //call when script is unactive
    private void OnDisable() {

        Stats.OnTakeDamage -= HandleTakeDamage;
        Stats.OnDie -= HandleDie;
    }

    //call whenever it take damage and change to impact state
    private void HandleTakeDamage() {

        Audio.PlaySound(Audio.Hit);
        SlowMo(0.15f);
        Instantiate(HitVfx, transform.position, HitVfx.transform.rotation);
        SwitchState(new FlyImpactState(this));
    }

    //call whenever die and change to dead state
    private void HandleDie() {

        Audio.PlaySound(Audio.Death);
        SwitchState(new FlyDeathState(this));
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

    private void OnDrawGizmosSelected() {

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, SpotRange);
        Gizmos.DrawWireSphere(transform.position, ShootRange);
        Gizmos.DrawWireSphere(transform.position, AttackRange); 
    }

    public IEnumerator FlyMeleeAttack(){

        Vector2 originPos = transform.position;
        Vector2 targetPos = astralStateMachine.transform.position;
        WeaponCollider.DisableWeapon();
        float percent = 0f;
        while (percent <= 1)
        {
            WeaponCollider.EnableWeapon();
            percent += Time.deltaTime * AttackSpeed;
            float interpolation = (-Mathf.Pow(percent, 2) + percent) * 4;
            transform.position = Vector3.Lerp(originPos, targetPos, interpolation);
            yield return null;
        }
    }
}
