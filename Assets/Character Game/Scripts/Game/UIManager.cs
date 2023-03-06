using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class UIManager : Singleton<UIManager> {
    [SerializeField] Slider HealthMeter;
    [SerializeField] TMP_Text ScoreUI;
    [SerializeField] TMP_Text LivesUI;
    [SerializeField] GameObject GameOverUI;
    [SerializeField] GameObject TitleUI;
    [SerializeField] GameObject GameWinUI;

    public void ShowTitle(bool Show = true) { 
        TitleUI.SetActive(Show);
    }

    public void ShowGameOver(bool Show = true) {
        GameOverUI.SetActive(Show);
    }

    public void ShowGameWin(bool Show = true) {
        GameWinUI.SetActive(Show);
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
}