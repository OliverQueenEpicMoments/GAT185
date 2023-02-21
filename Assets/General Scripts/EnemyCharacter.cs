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

    private void Start() {
        Target = GameObject.FindGameObjectWithTag("Player").transform;
        MainCamera = Camera.main;
        navmeshagent = GetComponent<NavMeshAgent>();

		GetComponent<Health>().OnDeath += OnDeath;
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

    void OnDeath() {
        StartCoroutine(Death());
    }

    IEnumerator Death() {
        animator.SetTrigger("Death");
        yield return new WaitForSeconds(4);
        Destroy(gameObject);
    }
}