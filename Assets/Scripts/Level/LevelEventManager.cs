using UnityEngine.Events;

/// <summary>
/// Ideally this would be an interface injected through DI, but this is a game jam & I don't have time to figure that 
/// out, so a static class is what we get! :)
/// </summary>
public static class LevelEventManager
{
    public static UnityEvent<GoalType, int> GoalAchieved = new();
    public static UnityEvent PlayerDied = new();
    public static UnityEvent PlayerShot = new();
    public static UnityEvent LevelStarted = new();
    public static UnityEvent LevelEnded = new();
}

public enum GoalType
{
    Main, 
    Side,
    Bonus
}