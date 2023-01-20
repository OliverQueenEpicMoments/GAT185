using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AsteroidGameManager : MonoBehaviour {
	[SerializeField] private TMP_Text ScoreUI;
	[SerializeField] private GameObject GameOverUI;

	int Score = 0;

	public void AddPoints(int Points) {
		Score += Points;
		ScoreUI.text = Score.ToString();
	}

	public void SetGameOver() {
		GameOverUI.SetActive(true);
	}
}
