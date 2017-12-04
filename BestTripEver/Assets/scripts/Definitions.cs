using System;

// ----------------------------------------------------------------
public enum EDirection
{
    Up,
    Right,
    Down,
    Left,
    No
}

// ----------------------------------------------------------------
public enum ESin
{
    Lust = 0,
    Gluttony,
    Greed,
    Sloth,
    Wrath,
    Envy,
    Pride,
}

// ----------------------------------------------------------------
public enum ETile
{
    Void = -4,
    In = -3,
    Out = -2,
    Map = -1,   // Definition only, unused. Will be used ESin index
}
