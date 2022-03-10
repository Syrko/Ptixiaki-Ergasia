using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Interface for Cards that have an effect on the hex where they spawn
/// </summary>
public interface IEffectWithTargetWhenSpawning
{
    public void ExecuteEffect(int depth, int width);
}
