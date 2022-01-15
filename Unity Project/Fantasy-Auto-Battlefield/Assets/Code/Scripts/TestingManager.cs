using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SpawnUnit();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnUnit()
    {
        UnitFactory.CreateUnitPawn("soldier", 0, 0);
        UnitFactory.CreateUnitPawn("soldier", 3, 4);
        UnitFactory.CreateUnitPawn("soldier", 1, 3);
        UnitFactory.CreateUnitPawn("soldier", 4, 6);

        BuildingFactory.CreateBuildingPawn("Gate", 0, 1);
        BuildingFactory.CreateBuildingPawn("Gate", 2, 3);
        BuildingFactory.CreateBuildingPawn("Gate", 2, 4);
        BuildingFactory.CreateBuildingPawn("Gate", 3, 2);
    }
}
