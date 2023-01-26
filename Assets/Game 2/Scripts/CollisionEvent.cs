using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionEvent : MonoBehaviour {
	[SerializeField] private string HitTagName = string.Empty;

	//public delegate void CollisionDelegate(GameObject other);
	public Action<GameObject> OnEnter;
	public Action<GameObject> OnExit;
	public Action<GameObject> OnStay;

	private void OnCollisionEnter(Collision collision) {
		if (HitTagName == string.Empty || collision.gameObject.CompareTag(HitTagName)) {
			OnEnter?.Invoke(collision.gameObject);
		}
	}

	private void OnCollisionExit(Collision collision) {
		if (HitTagName == string.Empty || collision.gameObject.CompareTag(HitTagName)) {
			OnExit?.Invoke(collision.gameObject);
		}
	}

	private void OnCollisionStay(Collision collision) {
		if (HitTagName == string.Empty || collision.gameObject.CompareTag(HitTagName)) {
			OnStay?.Invoke(collision.gameObject);
		}
	}

	private void OnTriggerEnter(Collider other) {
		if (HitTagName == string.Empty || other.gameObject.CompareTag(HitTagName)) {
			OnEnter?.Invoke(other.gameObject);
		}		
	}

	private void OnTriggerExit(Collider other) {
		if (HitTagName == string.Empty || other.gameObject.CompareTag(HitTagName)) {
			OnExit?.Invoke(other.gameObject);
		}
	}

	private void OnTriggerStay(Collider other) {
		if (HitTagName == string.Empty || other.gameObject.CompareTag(HitTagName)) {
			OnStay?.Invoke(other.gameObject);
		}
	}
}