using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CardData", menuName = "Card Data", order = 51)]
public class CardData : ScriptableObject
{
    [SerializeField]
    private string cardName;
    private int cardCost;
}
