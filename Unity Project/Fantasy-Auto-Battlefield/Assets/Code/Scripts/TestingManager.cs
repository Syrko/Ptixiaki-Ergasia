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
        UnitFactory.CreateUnitPawn(CardCatalog.Soldier, 0, 0);
        UnitFactory.CreateUnitPawn(CardCatalog.Soldier, 3, 4);
        UnitFactory.CreateUnitPawn(CardCatalog.Soldier, 1, 3);
        UnitFactory.CreateUnitPawn(CardCatalog.Soldier, 4, 6);

        BuildingFactory.CreateBuildingPawn(CardCatalog.Gate, 0, 1);
        BuildingFactory.CreateBuildingPawn(CardCatalog.Gate, 2, 3);
        BuildingFactory.CreateBuildingPawn(CardCatalog.Gate, 2, 4);
        BuildingFactory.CreateBuildingPawn(CardCatalog.Gate, 3, 2);
    }
}
