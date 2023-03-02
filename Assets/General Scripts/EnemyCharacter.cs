using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyCharacter : MonoBehaviour {
    [SerializeField] Animator animator;
    [SerializeField] Sensor sensor;
    [SerializeField] Transform AttackTransform;
    [SerializeField] float damage;

    private Camera MainCamera;
    private NavMeshAgent navmeshagent;
    private Transform Target;

    private State state = State.IDLE;
    private float Timer = 0;

    enum State {
        IDLE,
        PATROL,
        CHASE,
        ATTACK,
        DEATH
    }

    private void Start() {
        Target = GameObject.FindGameObjectWithTag("Player").transform;
        MainCamera = Camera.main;
        navmeshagent = GetComponent<NavMeshAgent>();

		GetComponent<Health>().OnDeath += OnDeath;
	}

    void Update() {
        switch (state) {
            case State.IDLE:
                state = State.PATROL;
                break;
            case State.PATROL:
                navmeshagent.isStopped = false;
                Target = GetComponent<WaypointNavigator>().waypoint.transform;
                if (sensor.Sensed != null) {
                    state = State.CHASE;
                }
                break;
            case State.CHASE:
                navmeshagent.isStopped = false;
                if (sensor.Sensed != null) { 
                    Target = sensor.Sensed.transform;
                    float Distance = Vector3.Distance(Target.position, transform.position);
                    if (Distance <= 2) {
                        StartCoroutine(Attack());
                    }
                    Timer = 2;
                }
                Timer -= Time.deltaTime;
                if (Timer < 0) {
                    state = State.PATROL;
                }
                break;
            case State.ATTACK:
                navmeshagent.isStopped = true;
                break;
            case State.DEATH:
                navmeshagent.isStopped = true;
                break;
            default:
                break;
        }
        navmeshagent.SetDestination(Target.position);
        animator.SetFloat("Speed", navmeshagent.velocity.magnitude); 
    }

    void OnDeath() {
        StartCoroutine(Death());
    }

    IEnumerator Death() {
        state = State.DEATH;
        animator.SetTrigger("Death");
        yield return new WaitForSeconds(4);
        Destroy(gameObject);
    }

    IEnumerator Attack() { 
        state = State.ATTACK;
        animator.SetTrigger("Attack");
        yield return new WaitForSeconds(4.0f);
        state = State.CHASE;
    }

	void OnAnimAttack() {
		var Colliders = Physics.OverlapSphere(AttackTransform.position, 2);
		foreach (var Collider in Colliders)	{
			if (Collider.gameObject.CompareTag("Player")) {
				if (Collider.gameObject.TryGetComponent<Health>(out Health health)) {
					health.OnApplyDamage(damage);
                    break;
				}
			}
		}
	}
}