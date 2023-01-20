using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : Collidable {
    [SerializeField] GameObject PickupFX;

    void Start() {
        OnEnter += OnCoinPickup;
    }

    void Update() {
        
    }

    void OnCoinPickup(GameObject Go) {
        if (Go.TryGetComponent<RollerPlayer>(out RollerPlayer Player)) {
            Player.AddPoints(100);
        }

        Instantiate(PickupFX, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}