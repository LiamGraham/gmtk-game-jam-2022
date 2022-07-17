using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public partial class GameManager : MonoBehaviour, IDisposable
{
    public List<GameObject> levelPrefabs = new() { };

    public GameState State { get; private set; }

    private void Start()
    {
        State = GameState.Starting;
        LevelEventManager.ObjectiveAchieved?.AddListener(ObjectiveAchieved);
    }

    void ObjectiveAchieved(GoalType goalType, int score)
    {
        if (goalType == GoalType.Main)
        {
            State = GameState.ObjectiveAchieved;
        }
    }

    public void Dispose()
    {
        LevelEventManager.ObjectiveAchieved?.RemoveListener(ObjectiveAchieved);
    }
}
