using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryCondition : Condition {
	[SerializeField] ItemData itemdata;

	public override bool IsTrue(GameObject interact) {
		if (interact.TryGetComponent<Inventory>(out Inventory inventory)) {
			if (inventory.Contains(itemdata)) return true;
		}
		return false;
	}
}