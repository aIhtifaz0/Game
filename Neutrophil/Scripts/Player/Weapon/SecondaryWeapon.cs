using UnityEngine;
using System;

[Serializable]
public class SecondaryWeapon : MonoBehaviour{
    [field: SerializeField] public SecondaryAttackData[] SecondaryAttack { get; private set; }
    [field: SerializeField] public SecondaryUltimateAttackData UltimateAttack { get; private set; }
}
/*
    getting reference for attack data from all attack scriptable object 
*/