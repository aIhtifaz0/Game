using UnityEngine;

[CreateAssetMenu(menuName = "Tertiary Ultimate Attack Data")]
public class TertiaryUltimateAttackData : ScriptableObject{
    [field: SerializeField] public string AnimationName { get; private set; }
    [field: SerializeField] public float TransitionDuration { get; private set; }
    [field: SerializeField] public float ComboAttackTime { get; private set; }
    [field: SerializeField] public float Force { get; private set; }
    [field: SerializeField] public float ForceTime { get; private set; }
    [field: SerializeField] public float Knockback { get; private set; }
    [field: SerializeField] public float UpKnockback { get; private set; }
    [field: SerializeField] public float ManaUsage { get; private set; }
    [field: SerializeField] public int Damage { get; private set; }
    [field: SerializeField] public int ComboStateIndex { get; private set; } = -1;
}
