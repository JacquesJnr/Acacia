using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    static int CurrentScore = 0;
    public int baseScoreIncrease;

    public TMP_Text score;
    public TMP_Text matches;
    public TMP_Text comboAmmount;

    [Header("Combo System")]
    public int combo = 1;
    public bool comboIncrease;
    public float comboTimer = 0;
    public float comboDuration = 10f;
    public Image comboMeter;

    public static int GetScore => CurrentScore;
    public static int SetScore(int score) => CurrentScore = score;

    private void OnEnable()
    {
        CardMatch.OnMatch += IncreaseScore;
        GameManager.OnStateChanged += OnGameStateChanged;

        score.text = CurrentScore.ToString();
    }
    
    private void OnGameStateChanged(GameState state)
    {
        if (state == GameState.Reset)
        {
            //CurrentScore = 0;
            combo = 1;
            comboTimer = -1;
            comboMeter.fillAmount = 0;
        }
    }

    public int CalulateNewScore(int score, int comboIncrease) => CurrentScore + (score * comboIncrease);

    private void IncreaseScore(int id)
    {
        CurrentScore = CalulateNewScore(baseScoreIncrease, combo);
        comboTimer = comboDuration;
        ++combo;
    }

    private void Update()
    {
        score.text = CurrentScore.ToString();
        comboAmmount.text = $"{combo}X";
        matches.text = GameManager.Instance.GetSaveData.matches.ToString();

        if (comboTimer >= 0)
        {
            comboTimer -= Time.deltaTime;
            float fillAmount = comboTimer / comboDuration;
            comboMeter.fillAmount = fillAmount;
        }
        else
        {
            comboTimer = -1;
            combo = 1;
        }
    }
    

    private void OnDisable()
    {
        CardMatch.OnMatch -= IncreaseScore;
    }
}
