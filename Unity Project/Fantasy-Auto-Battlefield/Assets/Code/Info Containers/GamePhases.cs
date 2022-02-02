using System;
using System.Collections.Generic;
using UnityEngine;

public enum GamePhases
{
    Upkeep_Phase = 0,
    Standard_Phase = 1,
    Move_Phase = 2,
    Combat_Phase = 3,
    End_Phase = 4
}

public static class GamePhasesExtension
{
    public static GamePhases NextPhase(this GamePhases currentPhase)
    {
        int nextPhase = (((int)currentPhase)+1)%Enum.GetNames(typeof(GamePhases)).Length;
        return (GamePhases)nextPhase;
    }

    public static string GetLabel(this GamePhases phase)
    {
        string[] stringParts = phase.ToString().Split('_');
        return stringParts[0] + " " + stringParts[1];
    }
}
