using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public enum GameState
{
    /// <summary>
    /// For an overview flyby of the game map
    /// </summary>
    Starting, 

    /// <summary>
    /// When the player has control 
    /// </summary>
    Playing,

    /// <summary>
    /// When the main objective of the map has been reached!
    /// </summary>
    LevelOver
}
