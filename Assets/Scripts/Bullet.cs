using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
    public float Speed = 60;

    void Start() {
        Destroy(gameObject, 5);
    }

    void Update() {
        transform.Translate(Vector3.forward * Speed * Time.deltaTime);
    }
}
