using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    [Range(1, 10), Tooltip("Controls the speed of the player")] public float Speed = 5;
    [Range(1, 10)] public float RotationRate = 135;
    public GameObject Prefab;
    public Transform BulletSpawnLocation;

    private void Awake() {
        Debug.Log("Awake");
    }

    void Start() {
        Debug.Log("Start");
    }

    void Update() {
        Vector3 Direction = Vector3.zero;

        Direction.z = Input.GetAxis("Vertical");

        Vector3 Rotation = Vector3.zero;
        Rotation.y = Input.GetAxis("Horizontal");

        Quaternion Rotate = Quaternion.Euler(Rotation * RotationRate * Time.deltaTime);
        transform.rotation = transform.rotation * Rotate;

        transform.Translate(Direction * Speed * Time.deltaTime);
        //transform.position += Direction * Speed * Time.deltaTime;

        if (Input.GetButton("Fire1")) {
            //Debug.Log("Pew");
            //GetComponent<AudioSource>().Play();
            GameObject Bullet = Instantiate(Prefab, BulletSpawnLocation.position, BulletSpawnLocation.rotation);
        }
    }
}