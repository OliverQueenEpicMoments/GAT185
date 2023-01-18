using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollerPlayer : MonoBehaviour {
    [SerializeField] private float MaxForce = 5;

    private Vector3 Force;
    private Rigidbody RB;

    void Start() {
        RB = GetComponent<Rigidbody>();
    }

    void Update() {
        Vector3 Direction = Vector3.zero;

        Direction.x = Input.GetAxis("Horizontal");
        Direction.z = Input.GetAxis("Vertical");

        Force = Direction * 10;

        if (Input.GetButtonDown("Jump")) { 
            RB.AddForce(Vector3.up * 10, ForceMode.Impulse);
        }
    }

	private void FixedUpdate() {
		RB.AddForce(Force);
	}
}
