using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveMovement : MonoBehaviour {
	public float Amplitude;
	public float Rate;
	public float SpinRate;

	Vector3 InitialPosition;
	float time;
	float Angle;

	void Start() {
		time = Random.Range(0f, 5f);
		Angle = Random.Range(0f, 360f);
		InitialPosition = transform.position;
	}

	void Update() {
		time += Time.deltaTime * Rate;
		Angle += Time.deltaTime * SpinRate;

		Vector3 Offset = Vector3.up * Mathf.Sin(time) * Amplitude;
		transform.position = InitialPosition + Offset;
		transform.rotation = Quaternion.AngleAxis(Angle, Vector3.up);
	}
}
