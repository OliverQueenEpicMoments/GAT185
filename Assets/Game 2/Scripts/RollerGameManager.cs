using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SocialPlatforms.Impl;

public class RollerGameManager : Singleton<RollerGameManager> {
	[Header("Events")]
	[SerializeField] EventRouter StartGameEvent;
	[SerializeField] EventRouter StopGameEvent;
	[SerializeField] EventRouter WinGameEvent;

	[SerializeField] Slider HealthMeter;
	[SerializeField] TMP_Text ScoreUI;
	[SerializeField] TMP_Text LivesUI;
	[SerializeField] GameObject GameOverUI;
	[SerializeField] GameObject TitleUI;

	[SerializeField] AudioSource GameMusic;

	[SerializeField] GameObject PlayerPrefab;
	[SerializeField] Transform PlayerStart;

	int Lives = 0;

	public enum State {
		TITLE,
		START_GAME,
		START_LEVEL,
		PLAY_GAME,
		PLAYER_DEAD,
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
				TitleUI.SetActive(true);
				Cursor.lockState = CursorLockMode.None;
				Cursor.visible = true;
				break;
			case State.START_GAME:
				TitleUI.SetActive(false);
				Cursor.lockState = CursorLockMode.Locked;
				Lives = 3;
				SetLivesUI(Lives);
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
			case State.GAME_OVER:
				GameOverUI.SetActive(true);
				StateTimer -= Time.deltaTime;
				if (StateTimer <= 0) { 
					GameOverUI.SetActive(false);
					state = State.TITLE;
				}
                break;
			default:
				break;
		}
	}

	public void SetHealth(int Health) { 
		HealthMeter.value = Mathf.Clamp(Health, 0, 100);
	}

	public void SetScore(int Score) {
		ScoreUI.text = Score.ToString();
	}

	public void SetLivesUI(int lives) { 
		LivesUI.text = lives.ToString();
	}

	public void SetPlayerDead() { 
		StopGameEvent.Notify();
		GameMusic.Stop();

		Lives--;
		SetLivesUI(Lives);
		state = (Lives == 0) ? State.GAME_OVER : State.PLAYER_DEAD;
		StateTimer = 3;
	}

	public void SetGameOver() {
		StopGameEvent.Notify();
		GameMusic.Stop();
		GameOverUI.SetActive(true);
		state = State.GAME_OVER;
		StateTimer = 3;
	}

	public void SetGameWin() {
		Debug.Log("Congratulations You Won!");
	}

	public void OnStartGame() { 
		state = State.START_GAME;
	}
}