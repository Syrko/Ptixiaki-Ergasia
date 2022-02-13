using System;
using System.Collections.Generic;
using UnityEngine;

public class SpellFactory
{
    static GameObject originalSpell = GameObject.Find("OriginalSpell");

    public static void ActivateSpell(string spellName, int x, int y)
    {
        GameObject spell = GameObject.Instantiate(originalSpell);
        spell.AddComponent<Spell>().InitializeSpell(spellName);
        Component specificSpell = spell.AddComponent(Type.GetType(spellName));

        if (specificSpell is IEffect)
        {
            ((IEffect)specificSpell).ExecuteEffect();
        }
        else if (specificSpell is IEffectWithTarget)
        {
            ((IEffectWithTarget)specificSpell).ExecuteEffect(y, x);
        }
    }
}
