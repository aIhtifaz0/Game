using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemsData", menuName = "ItemsData", order = 1)]
public class ItemsData : ScriptableObject
{
    public ItemCategory itemCategory;
    public ItemType itemType;
    public string itemName;
    [TextArea(15, 20)]
    public string itemDescription;
    [TextArea(10, 5)]
    public string[] itemDescriptionLevel;
    public Sprite itemIcon;
    public Sprite itemActiveIcon;

    public bool isItemBought;
    public int itemID;
    public int itemPrice;
    public int itemLevel;

    [Header("Zinc Zone")]
    public int[] zincCost;
    public stats itemStats;

    [Header("Protein Zone")]
    public bool isUpgradeable;
    public ItemsData(string itemName, string itemDescription, Sprite itemIcon, int itemID, int itemPrice, int itemLevel, string itemDescriptionLevel)
    {
        this.itemName = itemName;
        this.itemDescription = itemDescription;
        this.itemIcon = itemIcon;
        this.itemID = itemID;
        this.itemPrice = itemPrice;
        this.itemLevel = itemLevel;
        this.itemDescriptionLevel = new string[] { itemDescriptionLevel };
    }

}

[System.Serializable]
public class stats
{
    public int level;
    [Header("Basic")]
    public int[] health;
    public int[] attack;
    public int[] defense;

    [Header("Offensive")]

    public float[] attackSpeed;
    public float[] damagePerSecond;
    public float[] lifeSteal;
    public float[] critAttack;

    [Header("Defensive")]
    
    public int[] armor;
    public int[] maxHealth;
    public float[] trapDamageReduction;
    public int[] healthRegen;


    [Header("Caster")]
    public int[] manaRegen;
    public int[] spellDamage;
    public int[] maxMana;

    [Header("Lightning")]
    public bool isLightning;
    public int[] lightningDamage;

    [Header("Attack Range")]
    public float[] attackRange;

    [Header("GylcoCoin")]
    public bool isGlycoCoin;
    public int[] glycoCoinAddition;
    
    [Header("Resurrection")]
    public bool isResurrection;
    public int[] resurrectionHealth;

    public stats(int level, int[] health, int[] attack, int[] defense, float[] attackSpeed, float[] damagePerSecond, float[] lifeSteal, float[] critAttack, int[] armor, int[] maxHealth, float[] trapDamageReduction, int[] healthRegen, int[] manaRegen, int[] spellDamage, int[] maxMana, bool isLightning, int[] lightningDamage, float[] attackRange, bool isGlycoCoin, int[] glycoCoinAddition, bool isResurrection, int[] resurrectionHealth)
    {
        this.level = level;
        this.health = health;
        this.attack = attack;
        this.defense = defense;
        this.attackSpeed = attackSpeed;
        this.damagePerSecond = damagePerSecond;
        this.lifeSteal = lifeSteal;
        this.critAttack = critAttack;
        this.armor = armor;
        this.maxHealth = maxHealth;
        this.trapDamageReduction = trapDamageReduction;
        this.healthRegen = healthRegen;
        this.manaRegen = manaRegen;
        this.spellDamage = spellDamage;
        this.maxMana = maxMana;
        this.isLightning = isLightning;
        this.lightningDamage = lightningDamage;
        this.attackRange = attackRange;
        this.isGlycoCoin = isGlycoCoin;
        this.glycoCoinAddition = glycoCoinAddition;
        this.isResurrection = isResurrection;
        this.resurrectionHealth = resurrectionHealth;
    }

    
}

public enum ItemCategory
{
    shop,
    skill,
    zinc,
    protein
}
public enum ItemType
{
    Offensive,
    Defensive,
    caster,
}
