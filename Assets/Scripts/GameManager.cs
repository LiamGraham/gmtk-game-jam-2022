using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public partial class GameManager : MonoBehaviour, IDisposable
{
    public GameState State { get; private set; }

    private void Start()
    {
        State = GameState.Starting;
        LevelEventManager.ObjectiveAchieved?.AddListener(ObjectiveAchieved);
    }

    void ObjectiveAchieved()
    {
        State = GameState.ObjectiveAchieved;
        Debug.Log("Goal Reached!");
    }

    public void Dispose()
    {
        LevelEventManager.ObjectiveAchieved?.RemoveListener(ObjectiveAchieved);
    }
}
