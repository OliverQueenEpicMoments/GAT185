using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {
    [Range(1, 10)] public float MinTime = 3;
    [Range(1, 10)] public float MaxTime = 5;
    [Range(1, 100)] public float Radius = 100;
    public Transform SpawnLocation = null;
    public GameObject[] Prefabs;

    float SpawnTimer = 0;

    void Start() {
        SpawnTimer = Random.Range(MinTime, MaxTime);
    }

    void Update() {
        SpawnTimer -= Time.deltaTime;
        if (SpawnTimer <= 0) {
            SpawnTimer = Random.Range(MinTime, MaxTime);

            Vector3 position = SpawnLocation.position + Quaternion.AngleAxis(Random.value * 360.0f, Vector3.up) * (Vector3.forward * Radius);
            Instantiate(Prefabs[Random.Range(0, Prefabs.Length + 1)], position, Quaternion.identity);
        }
    }
}