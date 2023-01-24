using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollerPlayer : MonoBehaviour {
    [SerializeField] private Transform View;
    [SerializeField] private float MaxForce = 5;

    private int Score;
    private Vector3 Force;
    private Rigidbody RB;

    void Start() {
        RB = GetComponent<Rigidbody>();
        View = Camera.main.transform;
        Camera.main.GetComponent<RollerCamera>().SetTarget(transform);
    }

    void Update() {
        Vector3 Direction = Vector3.zero;

        Direction.x = Input.GetAxis("Horizontal");
        Direction.z = Input.GetAxis("Vertical");

        Quaternion ViewSpace = Quaternion.AngleAxis(View.rotation.eulerAngles.y, Vector3.up);
        Force = ViewSpace * (Direction * MaxForce);

        if (Input.GetButtonDown("Jump")) { 
            RB.AddForce(Vector3.up * 10, ForceMode.Impulse);
        }

        RollerGameManager.Instance.SetHealth(50);
    }

	private void FixedUpdate() {
		RB.AddForce(Force);
	}

    public void AddPoints(int Points) {
        Score += Points;
        RollerGameManager.Instance.SetScore(Score);
    }
}