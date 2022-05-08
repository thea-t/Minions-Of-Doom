using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum FloorItem
{
    Beast,
    Boss,
    Candle,
    Chest,
    Letter,
    MiniGame,
    Monster,
    Shop
}
public static class Player
{
    public static FloorItem FloorItem;
    public static GameObject CurrentLevel;
}
