using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell : Card
{
    // TODO implement Spell class
    SpellCardData cardData;

    string cardName;
    int cardCost;
    int originalCardCost;
    string cardText;
    protected CardType cardType;
    protected Sprite cardImage;

    public string CardName { get => cardName; set => cardName = value; }
    public int CardCost { get => cardCost; set => cardCost = value; }
    public int OriginalCardCost { get => originalCardCost; set => originalCardCost = value; }
    public string CardText { get => cardText; set => cardText = value; }
    public CardType CardType { get => cardType; set => cardType = value; }
    public Sprite CardImage { get => cardImage; set => cardImage = value; }
}
