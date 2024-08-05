using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class JumpAttack{

    [field: SerializeField] public string AnimationName { get; private set; }
    [field: SerializeField] public float TransitionDuration { get; private set; }
    [field: SerializeField] public float ComboAttackTime { get; private set; }
    [field: SerializeField] public float ForceTime { get; private set; }
    [field: SerializeField] public float ForwardForce { get; private set; }
    [field: SerializeField] public float JumpForce { get; private set; }
    [field: SerializeField] public float AttackKnockback { get; private set; }
    [field: SerializeField] public float AttackUpKnockback { get; private set; }
    [field: SerializeField] public int AttackDamage { get; private set; }
    [field: SerializeField] public int ComboStateIndex { get; private set; } = -1;
}
