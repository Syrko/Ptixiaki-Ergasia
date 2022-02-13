using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SpellCardData", menuName = "Spell Card Data", order = 52)]
public class SpellCardData : ScriptableObject
{
    public static SpellCardData GetSpellDataFromName(string spellName)
    {
        SpellCardData data = Resources.Load<SpellCardData>("Cards/Spells/" + spellName + "/" + spellName);
        return data;
    }

    [SerializeField] string cardName;
    [SerializeField] int cardCost;
    [SerializeField] string cardText;
    [SerializeField] CardType cardType;
    [SerializeField] Sprite cardImage;

    public string CardName { get => cardName; }
    public int CardCost { get => cardCost; }
    public string CardText { get => cardText; }
    public CardType CardType { get => cardType; }
    public Sprite CardImage { get => cardImage; }
}
