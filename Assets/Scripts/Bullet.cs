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

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Player")) return;

        Destroy(other.gameObject);
        Destroy(gameObject);
        Debug.Log(other.gameObject);
    }
}
