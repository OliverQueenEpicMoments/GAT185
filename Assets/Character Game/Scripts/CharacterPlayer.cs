using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class CharacterPlayer : MonoBehaviour {
    [SerializeField] private float Speed = 5;
    [SerializeField] private float HitForce = 2;
    [SerializeField] private float Gravity = Physics.gravity.y;
    [SerializeField] private float TurnRate = 10;
    [SerializeField] private float JumpHeight = 2;
	[SerializeField] private Animator animator;

    CharacterController charactercontroller;
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
    }

    void Update() {
        Vector3 Direction = Vector3.zero;
		Vector2 Axis = PlayerInput.Player.Move.ReadValue<Vector2>();

        Direction.x = Axis.x;
        Direction.z = Axis.y;

		Direction = MainCamera.transform.TransformDirection(Direction);

		if (charactercontroller.isGrounded)	{
			Velocity.x = Direction.x * Speed;
			Velocity.z = Direction.z * Speed;
			InAirTime = 0;
			if (PlayerInput.Player.Jump.triggered) {
				animator.SetTrigger("Jump");
				Velocity.y = Mathf.Sqrt(JumpHeight * -3 * Gravity);
				Velocity.x = Direction.x * (Speed * 0.75f);
				Velocity.z = Direction.z * (Speed * 0.75f);
			}
		} else {
			InAirTime += Time.deltaTime;
			Velocity.y += Gravity * Time.deltaTime;
		}

		charactercontroller.Move(Velocity * Time.deltaTime);
		Vector3 Look = Direction;
		Look.y = 0;
		if (Look.magnitude > 0) {
			transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Look), TurnRate * Time.deltaTime);
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
		Body.velocity = PushDir * HitForce;
	}

	public void OnJump(InputAction.CallbackContext context) {
		if (context.performed) Debug.Log("Jump");
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