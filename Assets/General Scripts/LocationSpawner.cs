using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LocationSpawner : Spawner2 {
    [SerializeField] GameObject SpawnPrefab;
    Transform[] Locations;

    private new void Start() {
        // Call spawner parent start 
        base.Start();

        // Get all transform children under this game object 
        // Gets parent transform also, uses linq to exclude parent 
		Locations = transform.GetComponentsInChildren<Transform>().Where(T => T != transform).ToArray();
	}

    public override void Clear() { 
        
    }

	public override void Spawn() {
        // Spawn at random location
        Transform T = Locations[Random.Range(0, Locations.Length - 1)];
        Instantiate(SpawnPrefab, T.position, T.rotation);
	}
}