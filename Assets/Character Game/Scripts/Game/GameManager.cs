using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SocialPlatforms.Impl;

public class GameManager : Singleton<GameManager> {
	[SerializeField] AudioSource GameMusic;

	[SerializeField] GameObject PlayerPrefab;
	[SerializeField] Transform PlayerStart;

	[Header("Events")]
	[SerializeField] EventRouter StartGameEvent;
	[SerializeField] EventRouter StopGameEvent;
	[SerializeField] EventRouter WinGameEvent;

	int Lives = 0;

	public enum State {
		TITLE,
		START_GAME,
		START_LEVEL,
		PLAY_GAME,
		PLAYER_DEAD,
		GAME_WIN,
		GAME_OVER
	}

	State state = State.TITLE;
	float StateTimer = 0;

	private void Start() {
		WinGameEvent.OnEvent += SetGameWin;
	}

	private void Update() {
		switch (state) {
			case State.TITLE:
				UIManager.Instance.ShowTitle(true);
				Cursor.lockState = CursorLockMode.None;
				Cursor.visible = true;
				break;
			case State.START_GAME:
                UIManager.Instance.ShowTitle(false);
				Cursor.lockState = CursorLockMode.Locked;
				Lives = 3;
                //UIManager.Instance.SetLivesUI(Lives);
				state = State.START_LEVEL;
                break;
			case State.START_LEVEL:
                StartGameEvent.Notify();
				GameMusic.Play();
                Instantiate(PlayerPrefab, PlayerStart.position, PlayerStart.rotation);
				state = State.PLAY_GAME;
                break;
			case State.PLAY_GAME:
				//
				break;
			case State.PLAYER_DEAD:
				StateTimer -= Time.deltaTime;
				if (StateTimer <= 0) {
					state = State.START_LEVEL;
				}
                break;
			case State.GAME_WIN:
                UIManager.Instance.ShowGameWin(true);
                StateTimer -= Time.deltaTime;
                if (StateTimer <= 0) {
                    UIManager.Instance.ShowGameWin(false);
                    state = State.TITLE;
                }
                break;
			case State.GAME_OVER:
                UIManager.Instance.ShowGameOver(true);
                StateTimer -= Time.deltaTime;
				if (StateTimer <= 0) { 
					UIManager.Instance.ShowGameOver(false);
					state = State.TITLE;
				}
                break;
			default:
				break;
		}
	}

	public void SetPlayerDead() { 
		StopGameEvent.Notify();
		GameMusic.Stop();

		Lives--;
		UIManager.Instance.SetLivesUI(Lives);
		state = (Lives == 0) ? State.GAME_OVER : State.PLAYER_DEAD;
		StateTimer = 3;
	}

	public void SetGameOver() {
		StopGameEvent.Notify();
		GameMusic.Stop();
		UIManager.Instance.ShowGameOver(true);
		state = State.GAME_OVER;
		StateTimer = 3;
	}

	public void SetGameWin() {
        StopGameEvent.Notify();
        GameMusic.Stop();
        UIManager.Instance.ShowGameWin(true);
        state = State.GAME_WIN;
        StateTimer = 3;
    }

	public void OnStartGame() { 
		state = State.START_GAME;
	}
}