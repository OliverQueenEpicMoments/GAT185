using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Spawner2 : MonoBehaviour {
    [SerializeField] private float SpawnMinTime;
    [SerializeField] private float SpawnMaxTime;
    [SerializeField] private bool EnableOnAwake = true;

    public bool SpawnEnabled { get; set; }
    private float SpawnTimer;

    protected void Start() {
        // Set initial timer
        SpawnTimer = Random.Range(SpawnMinTime, SpawnMaxTime);
        SpawnEnabled = EnableOnAwake;
    }

    void Update() {
        if (!SpawnEnabled) return;

        // Decrement spawn timer
        SpawnTimer -= Time.deltaTime;
        if (SpawnTimer < 0) { 
            // Reset spawn timer and spawn
            SpawnTimer = Random.Range(SpawnMinTime, SpawnMaxTime);
            Spawn();
        }
    }
    public abstract void Spawn();
    public abstract void Clear();
}