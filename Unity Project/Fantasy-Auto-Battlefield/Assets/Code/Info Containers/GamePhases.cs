using System;

/// <summary>
/// Enumeration of the game phases
/// </summary>
public enum GamePhases
{
    Upkeep_Phase = 0,
    Standard_Phase = 1,
    Move_Phase = 2,
    Combat_Phase = 3,
    End_Phase = 4
}

/// <summary>
/// Extension class for the GamePhases enum that provides functionality to get the name of the phase or the next phase
/// </summary>
public static class GamePhasesExtension
{
    /// <summary>
    /// Returns the next phase in the logical order of the game
    /// </summary>
    public static GamePhases NextPhase(this GamePhases currentPhase)
    {
        int nextPhase = (((int)currentPhase)+1)%Enum.GetNames(typeof(GamePhases)).Length;
        return (GamePhases)nextPhase;
    }

    /// <summary>
    /// Return the name of the game phase in a formatted string for UI usage
    /// </summary>
    public static string GetLabel(this GamePhases phase)
    {
        string[] stringParts = phase.ToString().Split('_');
        return stringParts[0] + " " + stringParts[1];
    }
}
