using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sensor : MonoBehaviour {
    [SerializeField] private Transform Origin;
    [SerializeField] private string TargetTag;
    [SerializeField] private float Distance;
    [SerializeField] private float SenseRate;

    public GameObject Sensed { get; private set; } = null;

    void Start() {
        StartCoroutine(SenseRoutine());
    }

    IEnumerator SenseRoutine() { 
        while (true) {
            Sense();
            yield return new WaitForSeconds(SenseRate);
        }
    }

    void Sense() {
        Sensed = null;

        Ray ray = new Ray(Origin.position, Origin.forward);
        if (Physics.Raycast(ray, out RaycastHit raycast, Distance)) {
            if (raycast.collider.CompareTag(TargetTag)) { 
                Sensed = raycast.collider.gameObject;
            }
        }
    }
}