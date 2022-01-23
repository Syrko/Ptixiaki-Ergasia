using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class Deck
{
    List<string> cards;
    BoardUI boardUI;
    DiscardPile discardPile;

    public Deck(List<string> deck)
    {
        boardUI = GameObject.FindObjectOfType<BoardUI>();
        cards = deck;
        boardUI.PlayerDeckCounter.text = cards.Count.ToString();
        ShuffleDeck();
    }

    public string DrawCard()
    {
        string drawnCard = cards[0];
        cards.RemoveAt(0);

        // Update the deck counter in the UI
        int newDeckCount = int.Parse(boardUI.PlayerDeckCounter.text) - 1;
        if(newDeckCount == 0)
        {
            ShuffleDiscardPileIntoDeck();
            newDeckCount = cards.Count;
        }
        boardUI.PlayerDeckCounter.text = newDeckCount.ToString();
        //---------------------------------

        return drawnCard;
    }

    public void AssignDiscardPile(DiscardPile discardPile)
    {
        this.discardPile = discardPile;
    }

    void ShuffleDeck()
    {
        cards = cards.OrderBy(x => UnityEngine.Random.value).ToList();
    }

    public void ShuffleCardsIntoDeck(List<string> newCards)
    {
        foreach(string card in newCards)
        {
            cards.Add(card);
            ShuffleDeck();
        }
    }

    void ShuffleDiscardPileIntoDeck()
    {
        ShuffleCardsIntoDeck(discardPile.Cards);
    }
}
