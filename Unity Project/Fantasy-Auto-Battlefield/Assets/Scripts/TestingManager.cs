using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingManager : MonoBehaviour
{
    // SPAWN UNIT-------------------
    [Header("Unit Spawn")]
    [SerializeField] GameObject PawnTemplate;
    [SerializeField] UnitCardData SoldierData;
    [SerializeField] UnitCardData GateData;
    //------------------------------

    // Start is called before the first frame update
    void Start()
    {
        SpawnUnit();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private GameObject spawnedUnit;
    void SpawnUnit()
    {
        spawnedUnit = Instantiate(PawnTemplate, new Vector3(0, 0.5f, 0), Quaternion.identity);
        spawnedUnit.AddComponent<Unit>().Initialize(SoldierData);

        spawnedUnit = Instantiate(PawnTemplate, new Vector3(0, 0.5f, 2*HexDimensions.r), Quaternion.identity);
        spawnedUnit.AddComponent<Unit>().Initialize(GateData);
    }
}
