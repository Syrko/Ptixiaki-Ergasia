using System;
using UnityEngine;

/// <summary>
/// <c>UnitFactory</c> is factory that produces Unit Pawns
/// </summary>
public class UnitFactory
{
    private static GameObject pawnTemplate = Resources.Load<GameObject>("Pawn Template");
    private static float pawnSpawnHeight = 0.7f;

    /// <summary>
    /// <c>CreateUnitPawn</c> instantiates a gameobject at the given board coordinates with a Unit script on it
    /// </summary>
    /// <param name="unitName">The name of the unit you want to create</param>
    /// <param name="x">The x coordinate (width) of board</param>
    /// <param name="y">The x coordinate (depth) of board</param>
    /// <param name="forPlayer">Set as true if the pawn is for the human player</param>
    /// <returns>The created pawn game object</returns>
    public static GameObject CreateUnitPawn(string unitName, int x, int y, bool forPlayer)
    {
        Vector3 spawnPos = BoardManager.TranslateCoordinates(x, y, pawnSpawnHeight);

        GameObject unit = GameObject.Instantiate(pawnTemplate, spawnPos, Quaternion.identity);
        unit.AddComponent<Unit>().InitializeUnitPawn(unitName, forPlayer);
        Component specificUnit = unit.AddComponent(Type.GetType(unitName));
        unit.name = unitName;

        if (specificUnit is IEffectWhenSpawning)
        {
            ((IEffectWhenSpawning)specificUnit).ExecuteEffect();
        }
        else if (specificUnit is IEffectWithTargetWhenSpawning)
        {
            ((IEffectWithTargetWhenSpawning)specificUnit).ExecuteEffect(y, x);
        }

        return unit;
    }
}
