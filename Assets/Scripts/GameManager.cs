using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public partial class GameManager : MonoBehaviour, IDisposable
{
    private GameState state;

    private void Start()
    {
        state = GameState.Starting;
        LevelEventManager.GoalAchieved?.AddListener(GoalReached);
    }

    void GoalReached()
    {
        state = GameState.ObjectiveAchieved;
        Debug.Log("Goal Reached!");
    }

    public void Dispose()
    {
        LevelEventManager.GoalAchieved?.RemoveListener(GoalReached);
    }
}
