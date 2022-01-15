using System;
using UnityEngine;

public class BuildingFactory : MonoBehaviour
{
    private static GameObject pawnTemplate = Resources.Load<GameObject>("Pawn Template");
    private static float pawnSpawnHeight = 0.7f;

    public static GameObject CreateBuildingPawn(string buildingName, int x, int y)
    {
        Vector3 spawnPos = BoardManager.TranslateCoordinates(x, y, pawnSpawnHeight);

        GameObject building = GameObject.Instantiate(pawnTemplate, spawnPos, Quaternion.identity);
        building.AddComponent<Building>().InitializeBuildingPawn(buildingName);
        building.AddComponent(Type.GetType(buildingName));
        building.name = buildingName;
        return building;
    }
}
