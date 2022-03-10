using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Interface for Cards that have an effect when they enter the game
/// </summary>
public interface IEffectWhenSpawning
{
    public void ExecuteEffect();
}
