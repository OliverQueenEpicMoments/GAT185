using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CollisionEvent))]
public class ItemPickup : Interactable {
	[SerializeField] ItemData ItemData;

	void Start() {
		GetComponent<CollisionEvent>().OnEnter += OnInteract;
	}

	public override void OnInteract(GameObject target) {
		if (target.TryGetComponent<Inventory>(out Inventory inventory)) {
			inventory.AddItem(ItemData);
		}

		if (interactFX != null) Instantiate(interactFX, transform.position, Quaternion.identity);
		if (destroyOnInteract) Destroy(gameObject);
	}


}