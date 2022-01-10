using System;
using System.Collections.Generic;
using UnityEngine;

public class UnitFactory
{
    public static void AddUnitComponents(string unitName, ref GameObject pawnTemplate)
    {
        pawnTemplate.AddComponent<Unit>().Initialize(unitName);
        pawnTemplate.AddComponent(Type.GetType(unitName));
        pawnTemplate.name = unitName;
    }
}
