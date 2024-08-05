using UnityEngine;
using System;

[Serializable]
public class AirAttackData {
    [field: SerializeField] public string AnimationName { get; private set; }
    [field: SerializeField] public float TransitionDuration { get; private set; }
    [field: SerializeField] public float ComboAttackTime { get; private set; }
    [field: SerializeField] public float Force { get; private set; }
    [field: SerializeField] public float ForceTime { get; private set; }
    [field: SerializeField] public float Knockback { get; private set; }
    [field: SerializeField] public int Damage { get; private set; }
    [field: SerializeField] public int ComboStateIndex { get; private set; } = -1;
}
/*
    Setting up all variable and reference for attack anim, force, etc.
*/