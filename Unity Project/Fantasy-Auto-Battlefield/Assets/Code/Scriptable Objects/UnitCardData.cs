using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UnitCardData", menuName = "Unit Card Data", order = 52)]
public class UnitCardData : ScriptableObject
{
    public static UnitCardData GetUnitDataFromName(string unitName)
    {
        UnitCardData data = Resources.Load<UnitCardData>("Cards/Units/" + unitName + "/" + unitName);
        return data;
    }

    [SerializeField] string cardName;
    [SerializeField] int cardCost;
    [SerializeField] string cardText;
    [SerializeField] CardType cardType;
    [SerializeField] int attack;
    [SerializeField] int defense;
    [SerializeField] int maxHitPoints;
    [SerializeField] HexPatternCodes attackPattern;
    [SerializeField] Sprite cardImage;

    public string CardName { get => cardName; }
    public int CardCost { get => cardCost; }
    public string CardText { get => cardText; }
    public CardType CardType { get => cardType; }
    public int Attack { get => attack; }
    public int Defense { get => defense; }
    public int MaxHitPoints { get => maxHitPoints; }
    public HexPatternCodes AttackPattern { get => attackPattern; }
    public Sprite CardImage { get => cardImage; }
}
