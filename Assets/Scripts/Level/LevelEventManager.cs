using UnityEngine.Events;

/// <summary>
/// Ideally this would be an interface injected through DI, but this is a game jam & I don't have time to figure that 
/// out, so a static class is what we get! :)
/// </summary>
public static class LevelEventManager
{
    public static UnityEvent ObjectiveAchieved = new();
    public static UnityEvent Died = new();
}
