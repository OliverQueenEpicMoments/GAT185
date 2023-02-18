using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class CharacterPlayer : MonoBehaviour {
	[SerializeField] private PlayerData playerdata;
	[SerializeField] private Animator animator;
	[SerializeField] private InputRouter inputrouter;
	[SerializeField] private Weapon weapon;
	//[SerializeField] private ItemManager itemmanager;

    CharacterController charactercontroller;
	Vector2 InputAxis;

	PlayerInputActions PlayerInput;
	Camera MainCamera;
	Vector3 Velocity = Vector3.zero;
    float InAirTime = 0;

	private void OnEnable() {
		PlayerInput.Enable();
	}

	private void OnDisable() {
		PlayerInput.Disable();
	}

	private void Awake() {
		PlayerInput = new PlayerInputActions();
	}

	void Start() {
        charactercontroller = GetComponent<CharacterController>();
		MainCamera = Camera.main;

		inputrouter.JumpEvent += OnJump;
		inputrouter.MoveEvent += OnMove;
		inputrouter.FireEvent += OnFire;
		inputrouter.FireStopEvent += OnFireStop;
	}

    void Update() {
        Vector3 Direction = Vector3.zero;

        Direction.x = InputAxis.x;
        Direction.z = InputAxis.y;

		Direction = MainCamera.transform.TransformDirection(Direction);

		if (charactercontroller.isGrounded)	{
			Velocity.x = Direction.x * playerdata.Speed;
			Velocity.z = Direction.z * playerdata.Speed;
			InAirTime = 0;

		} else {
			InAirTime += Time.deltaTime;
			Velocity.y += playerdata.Gravity * Time.deltaTime;
		}

		charactercontroller.Move(Velocity * Time.deltaTime);
		Vector3 Look = Direction;
		Look.y = 0;
		if (Look.magnitude > 0) {
			transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Look), playerdata.TurnRate * Time.deltaTime);
		}
		
		// Set animator parameters
		animator.SetFloat("Speed", charactercontroller.velocity.magnitude);
		animator.SetFloat("VelocityY", charactercontroller.velocity.y);
		animator.SetFloat("InAirTime", InAirTime);
		animator.SetBool("IsGrounded", charactercontroller.isGrounded);
    }

	void OnControllerColliderHit(ControllerColliderHit hit) {
		Rigidbody Body = hit.collider.attachedRigidbody;

		// No rigidbody
		if (Body == null || Body.isKinematic) {
			return;
		}

		// We dont want to push objects below us
		if (hit.moveDirection.y < -0.3)	{
			return;
		}

		// Calculate push direction from move direction,
		// we only push objects to the sides never up and down
		Vector3 PushDir = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);

		// If you know how fast your character is trying to move,
		// then you can also multiply the push velocity by that.

		// Apply the push
		Body.velocity = PushDir * playerdata.HitForce;
	}

	public void OnJump() {
		if (charactercontroller.isGrounded) {
			animator.SetTrigger("Jump");
			Velocity.y = Mathf.Sqrt(playerdata.JumpHeight * -3 * playerdata.Gravity);
		}
	}

	public void OnFire() {
		weapon.Use();
	}

	public void OnFireStop() {
		weapon.StopUse();
	}


	public void OnMove(Vector2 axis) {
		InputAxis = axis;
	}

	public void OnAnimEventItemUse() { 
		weapon.OnAnimEventItemUse();
	}

	public void OnLeftFootSpawn(GameObject go) { 
		Transform Bone = animator.GetBoneTransform(HumanBodyBones.LeftFoot);
		Instantiate(go, Bone.position, Bone.rotation);
	}

	public void OnRightFootSpawn(GameObject go) {
		Transform Bone = animator.GetBoneTransform(HumanBodyBones.RightFoot);
		Instantiate(go, Bone.position, Bone.rotation);
	}
}