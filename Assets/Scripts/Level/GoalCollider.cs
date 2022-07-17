using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GoalCollider : MonoBehaviour
{
    public GoalType type = GoalType.Main;
    public int score = 10;

    private bool goalReached = false;

    private void Start()
    {
        goalReached = false;
    }

    private void OnTriggerStay(Collider other)
    {
        var playerDiceComponent = other.GetPlayerDiceComponent();

        if (playerDiceComponent == null) return;

        // Goal reached condition: 
        // - Player is colliding
        // - Player is not moving
        if (!goalReached && playerDiceComponent.IsStationary())
        {
            goalReached = true;
            LevelEventManager.ObjectiveAchieved?.Invoke(type, score);
        }
    }
}
