using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {
    [SerializeField] float MaxHealth = 100;
    public float health; // { get; set; }
    private bool IsDead { get; set; }

    public Action OnDamage;
    public Action OnHeal;
    public Action OnDeath;

    private void Awake() {
        health = MaxHealth;
    }

    public void OnApplyDamage(float Damage) {
        if (IsDead) return;

        health -= Damage;
        health = Mathf.Clamp(health, 0, MaxHealth);
        OnDamage?.Invoke();
        if (health <= 0) {
            IsDead = true;
            OnDeath?.Invoke();
        }
    }

    public void OnApplyHealth(float Heal) {
        if (IsDead) return;

        health += Heal;
        health = Mathf.Clamp(health, 0, MaxHealth);
        OnHeal?.Invoke();
    }
}
