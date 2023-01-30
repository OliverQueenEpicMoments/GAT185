using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CollisionEvent))]
public class ForceEffector : Interactable {
    [SerializeField] Transform ForceTransform;
    [SerializeField] float Force;
    [SerializeField] bool OneTime = true;

    void Start() {
        GetComponent<CollisionEvent>().OnEnter = OnInteract;
        if (!OneTime) GetComponent<CollisionEvent>().OnStay = OnInteract;
    }

    public override void OnInteract(GameObject target) {
        if (target.TryGetComponent<Rigidbody>(out Rigidbody RB)) {
            ForceMode Mode = (OneTime) ? ForceMode.Impulse : ForceMode.Force;
            RB.AddForce(ForceTransform.rotation * Vector3.forward * Force, Mode);
        }
    }
}