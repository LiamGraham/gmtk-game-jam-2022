using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GoalCollider : MonoBehaviour
{
    private bool goalReached = false;

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
            LevelEventManager.GoalAchieved?.Invoke();
        }
    }
}
