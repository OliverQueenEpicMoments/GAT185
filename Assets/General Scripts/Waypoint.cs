using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Waypoint : MonoBehaviour {
	[SerializeField] Waypoint[] Waypoints;

	public void OnTriggerEnter(Collider other) {
		if (other.gameObject.TryGetComponent<WaypointNavigator>(out WaypointNavigator waypointNavigator)) {
			// If current navigator waypoint is this waypoint, set new random waypoint
			if (waypointNavigator.waypoint == this) {
				waypointNavigator.waypoint = Waypoints[Random.Range(0, Waypoints.Length)];
			}
		}
	}

	public static GameObject GetNearestGameObjectWithTag(Vector3 position, string tag) {
		GameObject[] GameObjects = GameObject.FindGameObjectsWithTag(tag);
		GameObjects = GameObjects.OrderBy(go => (go.transform.position - position).sqrMagnitude).ToArray();

		return (GameObjects.Length > 0) ? GameObjects[0] : null;
	}
}