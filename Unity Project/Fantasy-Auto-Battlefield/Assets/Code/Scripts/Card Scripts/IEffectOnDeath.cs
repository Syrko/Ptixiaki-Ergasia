using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// Interface for Cards that have an effect when they die
public interface IEffectOnDeath
{
    public void ExecuteDeathEffect();
}
