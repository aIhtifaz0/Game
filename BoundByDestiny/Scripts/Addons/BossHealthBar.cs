using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealthBar : MonoBehaviour{

    [field: SerializeField] public EnemyStats Stats { get; private set; }

    [SerializeField] private Slider _HB;

    private void Start(){

        SetMaxHealth();
    }

    private void Update(){

        UpdateHealthBar();
    }

    public void SetMaxHealth(){

        _HB.maxValue = Stats.maxHealth;
        _HB.value = Stats.maxHealth;
    }

    public void UpdateHealthBar(){

        Debug.Log("Setting");
        _HB.value = Stats.health;   
    }
}
