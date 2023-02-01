using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CollisionEvent))]
public class GameEventEffector : Interactable {
    [SerializeField] EventRouter GameEvent;
    [SerializeField] bool OneTime = true;

    void Start() {
        GetComponent<CollisionEvent>().OnEnter = OnInteract;
        if (!OneTime) GetComponent<CollisionEvent>().OnStay = OnInteract;
    }

    public override void OnInteract(GameObject target) {
        GameEvent?.Notify();
        if (interactFX != null) Instantiate(interactFX, transform.position, Quaternion.identity);
        if (destroyOnInteract) Destroy(gameObject);
    }
}