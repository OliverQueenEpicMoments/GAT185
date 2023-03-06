using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "InputRouter", menuName = "Game/InputRouter")]
public class InputRouter : ScriptableObject, PlayerInputActions.IPlayerActions {
	public UnityAction FireEvent;
	public UnityAction FireStopEvent;
	public UnityAction JumpEvent;
	public UnityAction JumpStopEvent;
	public UnityAction NextItemEvent;
	public UnityAction<Vector2> MoveEvent;
	public UnityAction<Vector2> LookEvent;

	private PlayerInputActions InputActions;

	private void OnEnable()	{
		if (InputActions == null) {
			InputActions = new PlayerInputActions();
			InputActions.Player.SetCallbacks(this);
			InputActions.Player.Enable();
		}
	}

	private void OnDisable() {
		InputActions.Player.Disable();
	}

	public void OnMove(InputAction.CallbackContext context) {
		MoveEvent.Invoke(context.ReadValue<Vector2>());
	}

	public void OnLook(InputAction.CallbackContext context) {
		LookEvent?.Invoke(context.ReadValue<Vector2>());
	}

	public void OnFire(InputAction.CallbackContext context) {
		switch (context.phase)
		{
			case InputActionPhase.Performed:
				FireEvent?.Invoke();
				break;
			case InputActionPhase.Canceled:
				FireStopEvent?.Invoke();
				break;
		}
	}

	public void OnJump(InputAction.CallbackContext context) {
		switch (context.phase) {
			case InputActionPhase.Performed:
				JumpEvent?.Invoke();
				break;
			case InputActionPhase.Canceled:
				JumpStopEvent?.Invoke();
				break;
		}
	}

    public void OnNextItem(InputAction.CallbackContext context) {
        if (context.phase == InputActionPhase.Performed) {
			NextItemEvent?.Invoke();
		}
    }
}