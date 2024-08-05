using UnityEngine;
using System;
using System.Collections;

public class PlayerStats : MonoBehaviour{

    [SerializeField] private PlayerStateMachine stateMachine;
    [SerializeField] private WeaponDamagePlayer player;

    public MainWeapon main;
    public SecondaryWeapon secondary;
    public TertiaryWeapon spell;

    [SerializeField] public int maxHealth = 100;
    [SerializeField] public int maxMana = 100;
    [SerializeField] public float health;

    public float healthRegen = 0f;
    public float manaRegen = 0.03f;
    [field :SerializeField] public float mana { get; set; }
    public float Health
    {
        get { return health; }
        set { health = value; }
    }

    public int MaxHealth
    {
        get { return maxHealth; }
        set { maxHealth = value; }
    }

    public int MaxMana
    {
        get { return maxMana; }
        set { maxMana = value; }
    }

    public PlayerStatsTotal playerStatsTotal;
    public int Heals;
    private bool isInvulnerable;

    private float timer;
    [HideInInspector]
    public int UltimatePoint;

    //return true if health equal zero 
    public bool isDie => health <= 0;

    //event for take damage
    public event Action OnTakeDamage;
    public event Action OnDie;
    

    private void Start(){ 

        main = stateMachine.MainWeapon;
        secondary = stateMachine.SecondaryWeapon;
        spell = stateMachine.TertiaryWeapon; 

        health = maxHealth;
        mana = maxMana;
        CheckHeals();
        //AddDamage();
        AddHealthAndMana();
    }

    private void Update() {

        if (timer > 0f) {

            timer -= Time.deltaTime;
        }
        else {

            timer = 5f;
        }if(UltimatePoint >= 100)
            SetUltimate();

        if(mana < maxMana){

            mana += manaRegen;
        }

        if(health < maxHealth){

            health += healthRegen;
        }
    }

    public void SetInvulnerable(bool isInvulnerable){

        this.isInvulnerable = isInvulnerable;
    }

    public void DealDamage(int damage){
        
        if(health == 0) { return; }

        if(isInvulnerable) { return; }
        
        //assign health after get damage or 0 when pass below than 0
        health = Mathf.Max(health - (damage - playerStatsTotal.armor), 0);

        //call the event
        OnTakeDamage?.Invoke();

        Instantiate(stateMachine.HitVfx, stateMachine.transform.position, stateMachine.HitVfx.transform.rotation);

        //after take damage check health again
        if(health <= 0){
            OnDie?.Invoke();
        }
    }

    public IEnumerator DamageOnStay(int damage){

        for(float currentHealth = health; currentHealth >= 0; currentHealth -= damage){

            OnTakeDamage?.Invoke();

            Instantiate(stateMachine.HitVfx, stateMachine.transform.position, stateMachine.HitVfx.transform.rotation);


            health = (int)currentHealth;
            yield return new WaitForSeconds(2f);
        }

        if(health <= 0){
            OnDie?.Invoke();
        }
        health = 0;
    }

    public void Heal(){

        //CheckHeals();
        Heals--;
        health = Mathf.Max(health + 20, 100);
    }

    public void ChargeUltimate(){

        UltimatePoint = Mathf.Max(UltimatePoint + 10, 100);
    }

    public void SetUltimate(){

        stateMachine.Ultimate++;
    }

    public void CheckHeals(){

        Heals = stateMachine.Heals;
    }

    public void AddDamage(){

        foreach(BasicAttackData basicAttack in main.BasicAttack){
            basicAttack.Damage += (int)playerStatsTotal.critAttack;
        }

        foreach(SecondaryAttackData secondaryAttack in secondary.SecondaryAttack){
            secondaryAttack.Damage += (int)playerStatsTotal.critAttack;
        }

        foreach(AirAttackData airAttack in main.AirAttack){
            airAttack.Damage += (int)playerStatsTotal.critAttack;
        }

        main.StrikeDownAttack.Damage += (int)playerStatsTotal.critAttack;
        main.StrikeUpAttack.Damage += (int)playerStatsTotal.critAttack;
        spell.TertiaryAttack.Damage += playerStatsTotal.spellDamage;
    }

    public void AddHealthAndMana(){

        maxHealth += playerStatsTotal.maxHealth;
        maxMana += playerStatsTotal.maxMana;

        manaRegen = playerStatsTotal.manaRegen;
        healthRegen = playerStatsTotal.healthRegen;
    }
    
}

[System.Serializable]
public struct PlayerStatsTotal
{
    private PlayerStats Stats;

    public int level;
    [Header("Basic")]
    public int health; 
    public int attack; 
    public int defense; 

    [Header("Offensive")]

    public float damagePerSecond; //racun ke health musuh
    public float lifeSteal; //nambah darah pas attack
    public float critAttack; //nambah damage

    [Header("Defensive")]
    
    public int armor; //Ngurangin damage musuh
    public int maxHealth; //nambah max health terus add ke health
    public float trapDamageReduction; //ngirangin damage trap
    public int healthRegen; //health regen


    [Header("Caster")]
    public int manaRegen; //ngecepetin mana regen
    public int spellDamage; //nambah damage spell
    public int maxMana; //nambah mana

    public void Add(ItemsData item)
    {
        // Assuming ItemsData has properties for health, attackPower, and defense
        var level = item.itemLevel;
        if (item.itemStats.health != null && level < item.itemStats.health.Length)
        health += item.itemStats.health[level];
        if (item.itemStats.attack != null && level < item.itemStats.attack.Length)
            attack += item.itemStats.attack[level];
        if (item.itemStats.defense != null && level < item.itemStats.defense.Length)
            defense += item.itemStats.defense[level];
        if (item.itemStats.damagePerSecond != null && level < item.itemStats.damagePerSecond.Length)
            damagePerSecond += item.itemStats.damagePerSecond[level];
        if (item.itemStats.lifeSteal != null && level < item.itemStats.lifeSteal.Length)
            lifeSteal += item.itemStats.lifeSteal[level];
        if (item.itemStats.critAttack != null && level < item.itemStats.critAttack.Length)
            critAttack += item.itemStats.critAttack[level];
        if (item.itemStats.armor != null && level < item.itemStats.armor.Length)
            armor += item.itemStats.armor[level];
        if (item.itemStats.maxHealth != null && level < item.itemStats.maxHealth.Length)
            maxHealth += item.itemStats.maxHealth[level];
        if (item.itemStats.trapDamageReduction != null && level < item.itemStats.trapDamageReduction.Length)
            trapDamageReduction += item.itemStats.trapDamageReduction[level];
        if (item.itemStats.healthRegen != null && level < item.itemStats.healthRegen.Length)
            healthRegen += item.itemStats.healthRegen[level];
        if (item.itemStats.manaRegen != null && level < item.itemStats.manaRegen.Length)
            manaRegen += item.itemStats.manaRegen[level];
        if (item.itemStats.spellDamage != null && level < item.itemStats.spellDamage.Length)
            spellDamage += item.itemStats.spellDamage[level];
        if (item.itemStats.maxMana != null && level < item.itemStats.maxMana.Length)
            maxMana += item.itemStats.maxMana[level];

    }

    public void Reset()
    {
        level = 0;
        health = 0;
        attack = 0;
        defense = 0;
        damagePerSecond = 0;
        lifeSteal = 0;
        critAttack = 0;
        armor = 0;
        maxHealth = 0;
        trapDamageReduction = 0;
        healthRegen = 0;
        manaRegen = 0;
        spellDamage = 0;
        maxMana = 0;
    }
}