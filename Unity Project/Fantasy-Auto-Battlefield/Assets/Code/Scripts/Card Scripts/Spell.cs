using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// <c>Spell</c> is monobehaviour inheriting from the Card class and is attached to 
/// the gameobjects representing a Spell card
/// </summary>
public class Spell : Card
{
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

    /// <summary>
    /// Initializes a new Spell with data from the appropriate Scriptable Object.
    /// Should be called after attaching the script to a gameobject.
    /// </summary>
    /// <param name="spellName">The name of the spell you are creating.</param>
    public void InitializeSpell(string spellName)
    {
        SpellCardData data = SpellCardData.GetSpellDataFromName(spellName);
        cardName = data.CardName;
        cardCost = data.CardCost;
        originalCardCost = data.CardCost;
        cardText = data.CardText;
        cardType = data.CardType;
        cardImage = data.CardImage;
    }
}
