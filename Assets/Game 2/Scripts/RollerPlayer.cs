using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class RollerPlayer : MonoBehaviour {
    [SerializeField] private Transform View;
    [SerializeField] private float MaxForce = 5;

    [SerializeField] private float GroundRayLength = 1;
    [SerializeField] private LayerMask GroundLayer;

    private int Score;
    private Vector3 Force;
    private Rigidbody RB;

    void Start() {
        RB = GetComponent<Rigidbody>();

        View = Camera.main.transform;
        Camera.main.GetComponent<RollerCamera>().SetTarget(transform);

        GetComponent<Health>().OnDamage += OnDamage;
        GetComponent<Health>().OnDeath += OnDeath;

        RollerGameManager.Instance.SetHealth((int)GetComponent<Health>().health);
    }

    void Update() {
        Vector3 Direction = Vector3.zero;

        Direction.x = Input.GetAxis("Horizontal");
        Direction.z = Input.GetAxis("Vertical");

        Quaternion ViewSpace = Quaternion.AngleAxis(View.rotation.eulerAngles.y, Vector3.up);
        Force = ViewSpace * (Direction * MaxForce);

        Ray ray = new Ray(transform.position, Vector3.down);
        bool OnGround = Physics.Raycast(ray, GroundRayLength, GroundLayer);
        Debug.DrawRay(transform.position, ray.direction * GroundRayLength);

        if (OnGround && Input.GetButtonDown("Jump")) { 
            RB.AddForce(Vector3.up * 10, ForceMode.Impulse);
        }
    }

	private void FixedUpdate() {
		RB.AddForce(Force);
	}

    public void AddPoints(int Points) {
        Score += Points;
        RollerGameManager.Instance.SetScore(Score);
    }

    public void OnDamage() {
        Debug.Log("Damage");
        RollerGameManager.Instance.SetHealth((int)GetComponent<Health>().health);
    }

    public void OnDeath() {
        RollerGameManager.Instance.SetGameOver();
    }
}