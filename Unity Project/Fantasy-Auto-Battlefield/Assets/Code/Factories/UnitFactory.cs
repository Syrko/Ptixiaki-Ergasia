using System;
using UnityEngine;

public class UnitFactory
{
    private static GameObject pawnTemplate = Resources.Load<GameObject>("Pawn Template");
    private static float pawnSpawnHeight = 0.7f;
    
    public static GameObject CreateUnitPawn(string unitName, int x, int y, bool forPlayer)
    {
        Vector3 spawnPos = BoardManager.TranslateCoordinates(x, y, pawnSpawnHeight);

        GameObject unit = GameObject.Instantiate(pawnTemplate, spawnPos, Quaternion.identity);
        unit.AddComponent<Unit>().InitializeUnitPawn(unitName, forPlayer);
        unit.AddComponent(Type.GetType(unitName));
        unit.name = unitName;
        return unit;
    }
}
