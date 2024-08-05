using UnityEngine;
using System;

[Serializable]
public class TertiaryWeapon : MonoBehaviour{
    [field: SerializeField] public TertiaryAttackData TertiaryAttack { get; private set; }
    [field: SerializeField] public TertiaryUltimateAttackData UltimateAttack { get; private set; }
}
/*
    getting reference for attack data from all attack scriptable object 
*/