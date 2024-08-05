using System.Collections;
using UnityEngine;

public class MeleeStateMachine : StateMachine {

    public enum Melee {
        NormalMelee, JumpMelee, StrongMelee, BossMelee, StayMelee
    }

    [field: Space(20)]
    [field: SerializeField] public Melee MeleeType { get; private set; }
    [field: SerializeField] public Animator Animator { get; private set; }
    [field: SerializeField] public Rigidbody2D Rigidbody { get; private set; }
    [field: SerializeField] public CapsuleCollider2D Collider { get; private set; }
    [field: Space(20)]
    [field: SerializeField] public PlayerControllerData ControllerData { get; private set; }
    [field: SerializeField] public MeleeForceReceiver ForceReceiver { get; private set; }
    [field: SerializeField] public WeaponDamageEnemy Weapon { get; private set; }
    [field: SerializeField] public AudioManager Audio { get; private set; }
    [field: SerializeField] public EnemyEdgeCheck EdgeCheck { get; private set; }
    [field: SerializeField] public EnemyStats Stats { get; private set; }
    [field: SerializeField] public Transform ShotPoint {get; private set; }
    [field: SerializeField] public GameObject HitVfx {get; private set; }
    [field: SerializeField] public GameObject DropVfx {get; private set; }
    [field: SerializeField] public GameObject DeathVfx {get; private set; }
    [field: SerializeField] public GameObject Summon {get; private set; }
    [field: SerializeField] public GameObject Projectile {get; private set; }
    [field: Space(20)]
    [field: SerializeField] public float SpotRange { get; private set; }
    [field: SerializeField] public float AttackRange { get; set; }
    [field: SerializeField] public float JumpRange { get; private set; }
    [field: SerializeField] public float MovementSpeed { get; private set; }
    [field: SerializeField] public float PatrolSpeed { get; private set; }
    [field: SerializeField] public float StunDuration { get; set; }
    [field: SerializeField] public float AfterSpecialDuration { get; set; }
    [field: SerializeField] public float AfterStunDuration { get; set; }
    [field: Space(20)]
    [field: SerializeField] public Transform[] RanPos { get; private set; }
    [field: SerializeField] public MeleeAttack[] Attacks { get; private set; }
    [field: SerializeField] public MeleeAttack[] DashAttacks { get; private set; }
    [field: SerializeField] public JumpAttack JumpAttack { get; private set; }
    [field: Space(20)]
    public PlayerStats Player { get; private set; }
    public PlayerStateMachine playerStateMachine { get; private set; }
    [field: Space(20)]

    public bool IsGrounded { get; set; } = false;
    public bool IsFacingRight { get; set; } //Player facing check
    public bool IsDashing { get;  set; } //Player dashing check
    public bool IsRecharging { get; set; }
    public bool IsShooting { get; set; }
    public bool IsSlowMotion { get;  set; }
    public bool IsSpecialDone { get;  set; } = false;
    public bool IsSpecial { get;  set; } = false;
    public bool IsUltimate { get;  set; } = false;

    public float specialDuration { get;  set; }
    [field:SerializeField] public float ultimateDuration { get;  set; }

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

        IsRecharging = false;
        specialDuration = AfterSpecialDuration;
        
        SwitchState(new MeleeIdleState(this));
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

        if(MeleeType != Melee.StayMelee || !IsUltimate){

            Audio.PlaySound(Audio.Hit);
            Instantiate(HitVfx, transform.position, HitVfx.transform.rotation);
            Instantiate(DropVfx, transform.position, DropVfx.transform.rotation);
            SwitchState(new MeleeImpactState(this));
        }
    }

    //call whenever die and change to dead state
    private void HandleDie() {
        Audio.PlaySound(Audio.Death);
        Instantiate(DeathVfx, transform.position, HitVfx.transform.rotation);
        SwitchState(new MeleeDeathState(this));
    }

    private void OnDrawGizmosSelected() {

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, SpotRange);
        Gizmos.DrawWireSphere(transform.position, AttackRange);
        Gizmos.DrawWireSphere(transform.position, JumpRange); 
    }


}
