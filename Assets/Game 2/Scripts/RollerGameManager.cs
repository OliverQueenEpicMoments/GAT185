using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SocialPlatforms.Impl;

public class RollerGameManager : Singleton<RollerGameManager> {
	[SerializeField] Slider HealthMeter;
	[SerializeField] TMP_Text ScoreUI;
	[SerializeField] GameObject GameOverUI;
	[SerializeField] GameObject TitleUI;

	[SerializeField] AudioSource GameMusic;

	[SerializeField] GameObject PlayerPrefab;
	[SerializeField] Transform PlayerStart;

	public enum State {
		TITLE,
		START_GAME,
		PLAY_GAME,
		GAME_OVER
	}

	State state = State.TITLE;
	float StateTimer = 0;

	private void Start() {
		
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
                Instantiate(PlayerPrefab, PlayerStart.position, PlayerStart.rotation);
				state = State.PLAY_GAME;
                break;
			case State.PLAY_GAME:
				break;
			case State.GAME_OVER:
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

	public void SetGameOver() {
		GameMusic.Stop();
		GameOverUI.SetActive(true);
		state = State.GAME_OVER;
		StateTimer = 3;
	}

	public void OnStartGame() { 
		state = State.START_GAME;
	}
}