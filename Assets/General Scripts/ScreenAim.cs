using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenAim : MonoBehaviour {
    [SerializeField] float Distance;

    private Camera MainCamera;

    void Start() {
        MainCamera = Camera.main;
    }

    void Update() {
        transform.position = MainCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, Distance));
    }
}