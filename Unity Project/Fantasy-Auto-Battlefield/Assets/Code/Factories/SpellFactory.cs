using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// <c>SpellFactory</c> is factory that activates Spells
/// </summary>
public class SpellFactory
{
    static GameObject originalSpell = GameObject.Find("OriginalSpell");

    /// <summary>
    /// <c>ActivateSpell</c> instantiates a gameobject with a Spell script and activates its effect
    /// at the given board coordinates
    /// </summary>
    /// <param name="spellName">The name of the spell you want to activate</param>
    /// <param name="x">The x coordinate (width) of board</param>
    /// <param name="y">The x coordinate (depth) of board</param>
    public static void ActivateSpell(string spellName, int x, int y)
    {
        GameObject spell = GameObject.Instantiate(originalSpell);
        spell.AddComponent<Spell>().InitializeSpell(spellName);
        Component specificSpell = spell.AddComponent(Type.GetType(spellName));

        if (specificSpell is IEffectWhenSpawning)
        {
            ((IEffectWhenSpawning)specificSpell).ExecuteEffect();
        }
        else if (specificSpell is IEffectWithTargetWhenSpawning)
        {
            ((IEffectWithTargetWhenSpawning)specificSpell).ExecuteEffect(y, x);
        }
    }
}
