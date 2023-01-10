using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    [Range(1, 10), Tooltip("Controls the speed of the player")] public float Speed = 5;
    public GameObject Prefab;

    private void Awake() {
        Debug.Log("Awake");
    }

    void Start() {
        Debug.Log("Start");
    }

    void Update() {
        Vector3 Direction = Vector3.zero;

        Direction.x = Input.GetAxis("Horizontal");
        Direction.z = Input.GetAxis("Vertical");
        //if (Input.GetKey(KeyCode.A)) Direction.x = -1;
        //if (Input.GetKey(KeyCode.D)) Direction.x = 1;
        //if (Input.GetKey(KeyCode.W)) Direction.z = 1;
        //if (Input.GetKey(KeyCode.S)) Direction.z = -1;

        transform.position += Direction * Speed * Time.deltaTime;

        if (Input.GetButton("Fire1")) {
            Debug.Log("Pew");
            GetComponent<AudioSource>().Play();
            GameObject Bullet = Instantiate(Prefab, transform.position, transform.rotation);
            Destroy(Bullet, 10);
        }
    }
}