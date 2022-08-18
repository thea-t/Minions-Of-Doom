using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Static class that stores players progress and stats.
/// </summary>
public static class Player
{
    public static DoorData SelectedDoor { get; set; }
    public static List<MinionBase> WonMinions = new List<MinionBase>();
    public static int CurrentLevel { get; set; }
    public static int StartingMana { get; set; } = 5;
    public static int StartingShield { get; set; } = 0;
    public static int StartingStrength { get; set; } = 0;
    public static int CurrentHealth { get; set; } = 100;
    public static int MaxHealth { get; set; } = 100;

}