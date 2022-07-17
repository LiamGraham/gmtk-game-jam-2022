using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public partial class GameManager : MonoBehaviour, IDisposable
{
    public GameObject playerCamera;
    public PlayerController playerController;

    public List<GameObject> levelPrefabs = new() { };
    public int levelIndex = 0;

    private GameObject currentLevel;

    public GameState State { get; private set; } = GameState.Starting;

    private void Start()
    {
        LevelEventManager.GoalAchieved?.AddListener(GoalAchieved);
        StartLevel();
    }

    private void StartLevel()
    {
        StartLevel(levelPrefabs[levelIndex % levelPrefabs.Count]);
    }

    private void StartLevel(GameObject levelPrefab)
    {
        State = GameState.Starting;

        // Destroy the current level if it exists
        if (currentLevel != null)
        {
            Destroy(currentLevel);
        }

        Debug.Log("Level Started");
        LevelEventManager.LevelStarted?.Invoke();
        playerController.State = PlayerState.Inactive;

        // Instantiate the new level
        currentLevel = Instantiate(levelPrefab, Vector3.zero, Quaternion.identity);

        var startPosition = currentLevel.GetComponentInChildren<StartPosition>();

        if (startPosition == null)
        {
            Debug.LogWarning($"{nameof(StartPosition)} not found on level. Please add a GameObject with a StartPosition provided");
            return;
        }

        playerController.SetPlayerPosition(startPosition.Position, startPosition.Rotation);

        // TODO Stretch goal: Play overview / flyby animation?

        // Play level start zoom animation
        if (playerCamera != null)
        {
            playerCamera.transform.SetPositionAndRotation(startPosition.Position + Vector3.up * 10, Quaternion.LookRotation(Vector3.down));
            var camera = playerCamera.GetComponent<FollowPlayer>();
            var cameraZoom = playerCamera.GetComponent<ZoomCameraIn>();

            if (cameraZoom != null && camera != null)
            {
                cameraZoom.RunZoom();
                cameraZoom.OnZoomFinished?.AddListener(OnZoomFinished);
            }
        }
    }

    private void OnZoomFinished()
    {
        var cameraZoom = playerCamera.GetComponent<ZoomCameraIn>();
        cameraZoom.OnZoomFinished?.RemoveListener(OnZoomFinished);

        State = GameState.Playing;
        playerController.State = PlayerState.Aiming;
    }

    void GoalAchieved(GoalType goalType, int score)
    {
        if (goalType == GoalType.Main)
        {
            State = GameState.LevelOver;
            LevelEventManager.LevelEnded?.Invoke();

            // TODO: show level end UI

            levelIndex++;
            StartLevel();
        }
    }

    public void Dispose()
    {
        LevelEventManager.GoalAchieved?.RemoveListener(GoalAchieved);
    }
}
