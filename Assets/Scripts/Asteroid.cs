using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour {
    public float Speed = 20;
    public float RotationRate = 20;
    public GameObject Explosion;

    Vector3 Direction;
    Vector3 Rotation;

    private void OnDestroy() {
        Instantiate(Explosion, transform.position, transform.rotation);
    }

    void Start() {
        Direction = Quaternion.AngleAxis(Random.Range(0, 360), Vector3.up) * Vector3.forward;
        Rotation = new Vector3(Random.Range(-1, 1), Random.Range(-1, 1), Random.Range(-1, 1));
    }

    void Update() {
        transform.position += Direction * Speed * Time.deltaTime;
        transform.rotation = transform.rotation * Quaternion.Euler(Rotation * RotationRate * Time.deltaTime);
    }
}