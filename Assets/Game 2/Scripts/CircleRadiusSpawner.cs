using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleRadiusSpawner : Spawner2 {
    [SerializeField] [Range(1, 1000)] private float Radius = 100;
    [SerializeField] private Transform SpawnLocation = null;
    [SerializeField] private GameObject[] Prefabs;

    public override void Spawn() {
        // Set spawn position around spawn location transform (player) at circle radius (distance) 
        Vector3 Position = SpawnLocation.position + Quaternion.AngleAxis(Random.value * 360.0f, Vector3.up) * (Vector3.forward * Radius);

        // Create spawn object from random spawn prefab, spawner is parent object
        Instantiate(Prefabs[Random.Range(0, Prefabs.Length)], Position, Quaternion.identity, transform);
    }

    public override void Clear() {
        // Get all children game objects of spawner
        var Spawned = GetComponentsInChildren<Transform>();

        // Iterate through all spawned transforms
        foreach (var Spawn in Spawned) {
            // Destroy child game object
            Destroy(Spawn.gameObject);
        }
    }
}