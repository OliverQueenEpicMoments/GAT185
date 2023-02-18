using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyCharacter : MonoBehaviour {
    [SerializeField] Animator animator;
    private Camera MainCamera;
    private NavMeshAgent navmeshagent;
    private Transform Target;

    void Start() {
        Target = GameObject.FindGameObjectWithTag("Player").transform;
        MainCamera = Camera.main;
        navmeshagent = GetComponent<NavMeshAgent>();
    }

    void Update() {
        navmeshagent.SetDestination(Target.position);
        animator.SetFloat("Speed", navmeshagent.velocity.magnitude);

        //if (Input.GetMouseButtonDown(0)) { 
        //    Ray ray = MainCamera.ScreenPointToRay(Input.mousePosition);
        //    if (Physics.Raycast(ray, out RaycastHit Hit)) { 
        //        navmeshagent.SetDestination(Hit.point);
        //    }
        //}    
    }
}