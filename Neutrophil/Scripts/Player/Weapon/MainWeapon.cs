using UnityEngine;
using System;

[Serializable]
public class MainWeapon : MonoBehaviour{
    [field: SerializeField] public BasicAttackData[] BasicAttack { get; set; }
    [field: SerializeField] public AirAttackData[] AirAttack { get; set; }
    [field: SerializeField] public StrikeUpAttackData StrikeUpAttack { get; set; }
    [field: SerializeField] public StrikeDownAttackData StrikeDownAttack { get; set; }
    [field: SerializeField] public MainUltimateAttackData UltimateAttack { get; set; }
}

/*
    getting reference for attack data from all attack scriptable object 
*/
