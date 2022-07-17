using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ParCounter : MonoBehaviour
{
    private int score = 0;

    // Start is called before the first frame update
    void Start()
    {
        PlayerDice.Instance.OnPlayerStationary.AddListener(PlayerStationary);
        LevelEventManager.GoalAchieved.AddListener(GoalAchieved);
        score = 0;
    }

    private void PlayerStationary(int arg0)
    {
        score++;
        UpdateScore();
    }

    private void UpdateScore()
    {
        TextMeshProUGUI textMeshProUGUI = GetComponent<TextMeshProUGUI>();
        textMeshProUGUI.text = score.ToString();
    }

    private void GoalAchieved(GoalType type, int value)
    {
        if (type == GoalType.Main)
        {
            score = 0;
            UpdateScore();
        }
    }

}
