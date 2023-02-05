using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CollisionEvent))]
public class AnimationEffector : Interactable {
	[SerializeField] Animator animator;
	[SerializeField] string Parameter;
	
	private void Start() {
		GetComponent<CollisionEvent>().OnEnter += OnInteract;
		GetComponent<CollisionEvent>().OnExit += OnInteract;
	}

	public override void OnInteract(GameObject target) {
		animator.SetTrigger(Parameter);

		if (interactFX != null) Instantiate(interactFX, transform.position, Quaternion.identity);
		if (destroyOnInteract) Destroy(gameObject);
	}
}