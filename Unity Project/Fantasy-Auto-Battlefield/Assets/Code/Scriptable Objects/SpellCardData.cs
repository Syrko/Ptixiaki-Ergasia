using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SpellCardData", menuName = "Spell Card Data", order = 52)]
public class SpellCardData : ScriptableObject
{
    public static SpellCardData GetSpellDataFromName(string spellName)
    {
        SpellCardData data = Resources.Load<SpellCardData>("Cards/Buildings/" + spellName + "/" + spellName);
        return data;
    }

    [SerializeField] string cardName;
    [SerializeField] int cardCost;
    [SerializeField] string cardText;
    [SerializeField] CardType cardType;
    [SerializeField] string cardID;
    [SerializeField] HexPatternCodes targetPattern;
    [SerializeField] Sprite cardImage;
}
