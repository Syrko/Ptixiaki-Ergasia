using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class Deck
{
    private static readonly System.Random rng = new System.Random();

    List<string> cards;

    public Deck(List<string> deck)
    {
        cards = deck;
        ShuffleDeck();
    }

    public string DrawCard()
    {
        string drawnCard = cards[0];
        cards.RemoveAt(0);
        return drawnCard;
    }

    void ShuffleDeck()
    {
        cards = cards.OrderBy(x => UnityEngine.Random.value).ToList();
    }

    void ShuffleCardsIntoDeck(List<string> newCards)
    {
        // TODO implement
        throw new NotImplementedException();
    }

    void GetCardsNumInDeck()
    {
        // TODO implement
        throw new NotImplementedException();
    }
}
