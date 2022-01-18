using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SpellCardData", menuName = "Spell Card Data", order = 52)]
public class SpellCardData : ScriptableObject
{
    [SerializeField] string cardName;
    [SerializeField] int cardCost;
    [SerializeField] string cardText;
    [SerializeField] CardType cardType;
    [SerializeField] string cardID;
    [SerializeField] HexPatternCodes targetPattern;
    [SerializeField] Sprite cardImage;
}
