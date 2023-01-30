using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PrefabSpawner : Spawner2 {
    [SerializeField] EventRouter StartEvent;
    [SerializeField] private Transform SpawnersParent;
    [SerializeField] private Transform SpawnedParent;

    private List<GameObject> Spawners = new List<GameObject>();

    private void OnEnable() {
        // Add spawn events
        if (StartEvent != null) StartEvent.OnEvent += StartSpawn;
    }

    private void OnDisable() {
        // Remove spawn events
        if (StartEvent != null) StartEvent.OnEvent -= StartSpawn;
    }

    private new void Start() {
        // Call parent start
        base.Start();

        // Get all children game object of spawners parent and add to spawners list
        foreach (Transform Child in SpawnedParent) {
            Spawners.Add(Child.gameObject);
        }

        // Set all spawners inactive (hide)
        foreach (GameObject Spawner in Spawners) {
            Spawner.SetActive(false);
        }
    }

    public override void Spawn() {
        // Find open prefab (No objects at prefab location)
        GameObject Prefab = GetRandomOpenSpawnPrefab();

        // Create spawn game object, set spawner as parent
        GameObject Spawn = Instantiate(Prefab, SpawnedParent);
        Spawn.SetActive(true);
    }

    public override void Clear() {
        // Go through all children of spawned parents and destroy children game objects
        foreach (Transform Child in SpawnedParent) { 
            Destroy(Child.gameObject);
        }
    }

    public void StartSpawn() {
        // Clear all spawned
        Clear();

        // Spawn all spawner objects
        foreach (var Prefab in Spawners) {
            // Spawn game object under spawned parent
            GameObject Spawn = Instantiate(Prefab.gameObject, SpawnedParent);

            Spawn.SetActive(true);
        }
    }

    private bool IsGameObjectOpen(GameObject Go) {
        // Check if there are any colliders at game object location
        return Physics.CheckSphere(Go.transform.position, 0.2f);
    }

    private GameObject GetRandomOpenSpawnPrefab() {
        GameObject OpenPrefab = null;
        int Attempts = 0;
         
        // Look for open prefab (No objects colliding at location)
        // Avoid infinite loop (No open prefabs) using attempts
        while (OpenPrefab == null && Attempts++ < Spawners.Count * 2) {
            // Get random prefab
            GameObject Prefab = Spawners[Random.Range(0, Spawners.Count)];

            // If prefab is open set open prefab
            if (IsGameObjectOpen(Prefab)) {
                OpenPrefab = Prefab;
            }
        }
        return OpenPrefab;
    }
}