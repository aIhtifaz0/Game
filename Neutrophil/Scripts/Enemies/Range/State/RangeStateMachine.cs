using System.Collections;
using UnityEngine;

public class RangeStateMachine : StateMachine {

    public enum Range {
        NormalRange, MeleeRange, EliteRange, StayRange
    }

    [field: SerializeField] public Range RangeType { get; private set; }
    [field: SerializeField] public Animator Animator { get; private set; }
    [field: SerializeField] public Rigidbody2D Rigidbody { get; private set; }
    [field: SerializeField] public CapsuleCollider2D Collider { get; private set; }
    [field: SerializeField] public AudioManager Audio { get; private set; }
    [field: SerializeField] public WeaponDamageEnemy Weapon { get; private set; }
    [field: SerializeField] public EnemyEdgeCheck EdgeCheck { get; private set; }
    [field: SerializeField] public EnemyStats Stats { get; private set; }
    [field: SerializeField] public RangeForceReceiver ForceReceiver { get; private set; }
    [field: SerializeField] public PlayerControllerData ControllerData { get; private set; }
    [field: SerializeField] public float SpotRange { get; private set; }
    [field: SerializeField] public float FleeRange {get; private set; }
    [field: SerializeField] public float AttackRange {get; private set; }
    [field: SerializeField] public float ShootRange { get; private set; }
    [field: SerializeField] public float MovementSpeed { get; private set; }
    [field: SerializeField] public float PatrolSpeed { get; private set; }
    [field: SerializeField] public int StrongBullet { get; set; }
    [field: SerializeField] public Transform ShotPoint { get; private set; }
    [field: SerializeField] public GameObject Bullet { get; private set; }
    [field: SerializeField] public GameObject HitVfx {get; private set; }
    [field: SerializeField] public GameObject DropVfx {get; private set; }
    [field: SerializeField] public GameObject MuzzleVfx {get; private set; }
    [field: SerializeField] public GameObject DeathVfx {get; private set; }
    [field: SerializeField] public Transform[] RanPos { get; private set; }
    [field: SerializeField] public RangeAttack Attack { get; private set; }

    public PlayerStats Player { get; private set; }
    public PlayerStateMachine playerStateMachine { get; private set; }

    public bool IsGrounded { get; set; } = false;
    public bool IsFacingRight { get; set; } //Player facing check
    public bool IsDashing { get;  set; } //Player dashing check
    public bool IsAttacking { get;  set; }
    public bool IsShooting { get;  set; }
    public bool IsChargeAttacking { get; set; }
    public bool IsReloading { get;  set; } //Player dashing check
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
        playerStateMachine = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStateMachine>();
    }

    private void Start(){

        IsReloading = false;
        IsShooting = false;
        IsAttacking = false;
        IsChargeAttacking = false;
        
        SwitchState(new RangeIdleState(this));
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
        
        Instantiate(HitVfx, transform.position, HitVfx.transform.rotation);
        Instantiate(DropVfx, transform.position, DropVfx.transform.rotation);
        SwitchState(new RangeImpactState(this));
    }

    //call whenever die and change to dead state
    private void HandleDie() {

        Audio.PlaySound(Audio.Death);

        if(DeathVfx != null){
            Instantiate(DeathVfx, transform.position, HitVfx.transform.rotation);
        }
        SwitchState(new RangeDeathState(this));
    }

    private void OnDrawGizmosSelected() {

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, SpotRange);
        Gizmos.DrawWireSphere(transform.position, FleeRange);
        Gizmos.DrawWireSphere(transform.position, AttackRange);
        Gizmos.DrawWireSphere(transform.position, ShootRange);
    }
}
