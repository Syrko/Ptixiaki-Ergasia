using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BuildingCardData", menuName = "Building Card Data", order = 52)]
public class BuildingCardData : ScriptableObject
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
