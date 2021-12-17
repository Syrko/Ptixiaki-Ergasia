using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UnitCardData", menuName = "Unit Card Data", order = 52)]
public class UnitCardData : ScriptableObject
{
    [SerializeField] string cardName;
    [SerializeField] int cardCost;
    [SerializeField] string cardText;
    [SerializeField] CardType cardType;
    [SerializeField] string cardID;
    [SerializeField] int attack;
    [SerializeField] int defense;
    [SerializeField] int maxHitPoints;
    [SerializeField] HexPattern attackPattern;
    [SerializeField] Sprite cardImage;
}
