using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointNavigator : MonoBehaviour {
    public Waypoint waypoint { get; set; }

    void Start() {
        GameObject GO = Waypoint.GetNearestGameObjectWithTag(transform.position, "Waypoint");
        waypoint = GO.GetComponent<Waypoint>();
    }
}