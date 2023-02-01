using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CollisionEvent))]
public class Damage : Interactable {
    [SerializeField] float damage = 0;
    [SerializeField] bool OneTime = true;

    void Start() {
        GetComponent<CollisionEvent>().OnEnter += OnInteract;
        if (!OneTime) GetComponent<CollisionEvent>().OnStay += OnInteract;

    }

    public override void OnInteract(GameObject Target) {
        if (Target.TryGetComponent<Health>(out Health health)) {
            health.OnApplyDamage(damage * ((OneTime) ? 1 : Time.deltaTime));
        }
    }
}