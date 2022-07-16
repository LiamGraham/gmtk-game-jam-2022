using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public static class ComponentUtilities
{
    /// <summary>
    /// Checks if this component is the player, i.e. it contains the PlayerDice component
    /// </summary>
    public static bool IsPlayer(this Component component)
    {
        return component.GetPlayerDiceComponent() != null;
    }

    /// <summary>
    /// Gets the PlayerDice component from this component. Returns null if this component does not have a player
    /// </summary>
    public static PlayerDice GetPlayerDiceComponent(this Component component)
    {
        return component.GetComponent<PlayerDice>();
    }
}
