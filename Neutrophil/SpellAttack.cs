using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellAttack : MonoBehaviour
{
    [SerializeField] private GameObject particle;
    [SerializeField] private WeaponDamagePlayer Weapon;

    [SerializeField] private int damage;
    [SerializeField] private int knockback;
    [SerializeField] private int upKnockback;
    // Start is called before the first frame update

    private void Start(){

        //setting attack value to weapondamage script base on attack class
        Weapon.SetAttack(damage, knockback, upKnockback, 0f, 0f);

        Destroy(gameObject, 0.5f);
    }
}
