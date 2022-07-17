using UnityEngine;

public class Level : MonoBehaviour, IDisposable
{
    public int points = 0;
    public int par = 1;
    public int shots = 0;
    public bool isActive;

    public void Start() {
        isActive = true;
        LevelEventManager.Shot?.AddListener(OnShot);
    }

    private void OnShot() {
        if (!isActive) {
            return;
        }
        shots++;
        Debug.Log("Shots: " + shots);   
    }

    public void Dispose()
    {
        LevelEventManager.Shot?.RemoveListener(OnShot);
    }
}