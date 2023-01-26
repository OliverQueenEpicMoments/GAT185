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

	[SerializeField] GameObject PlayerPrefab;
	[SerializeField] Transform PlayerStart;

	private void Start() {
		Instantiate(PlayerPrefab, PlayerStart.position, PlayerStart.rotation);
	}

	public void SetHealth(int Health) { 
		HealthMeter.value = Mathf.Clamp(Health, 0, 100);
	}

	public void SetScore(int Score) {
		ScoreUI.text = Score.ToString();
	}

	public void SetGameOver() {
		GameOverUI.SetActive(true);
	}
}