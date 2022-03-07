using System;
using UnityEngine;

/// <summary>
/// <c>BuildingFactory</c> is factory that produces Building Pawns
/// </summary>
public class BuildingFactory : MonoBehaviour
{
    private static GameObject pawnTemplate = Resources.Load<GameObject>("Pawn Template");
    private static float pawnSpawnHeight = 0.7f;

    /// <summary>
    /// <c>CreateBuildingPawn</c> instantiates a gameobject at the given board coordinates with a Building script on it
    /// </summary>
    /// <param name="buildingName">The name of the building you want to create</param>
    /// <param name="x">The x coordinate (width) of board</param>
    /// <param name="y">The x coordinate (depth) of board</param>
    /// <param name="forPlayer">Set as true if the pawn is for the human player</param>
    /// <returns>The created pawn game object</returns>
    public static GameObject CreateBuildingPawn(string buildingName, int x, int y, bool forPlayer)
    {
        Vector3 spawnPos = BoardManager.TranslateCoordinates(x, y, pawnSpawnHeight);

        GameObject building = GameObject.Instantiate(pawnTemplate, spawnPos, Quaternion.identity);
        building.AddComponent<Building>().InitializeBuildingPawn(buildingName, forPlayer);
        Component specificBuilding = building.AddComponent(Type.GetType(buildingName));
        building.name = buildingName;

        if (specificBuilding is IEffectWhenSpawning)
        {
            ((IEffectWhenSpawning)specificBuilding).ExecuteEffect();
        }
        else if (specificBuilding is IEffectWithTargetWhenSpawning)
        {
            ((IEffectWithTargetWhenSpawning)specificBuilding).ExecuteEffect(y, x);
        }

        return building;
    }
}
