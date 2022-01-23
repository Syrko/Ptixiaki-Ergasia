using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class Deck
{
    List<string> cards;
    BoardUI boardUI;

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
            // TODO implement re-shuffling
            throw new System.NotImplementedException();
            newDeckCount = cards.Count;
        }
        boardUI.PlayerDeckCounter.text = newDeckCount.ToString();
        //---------------------------------

        return drawnCard;
    }

    void ShuffleDeck()
    {
        cards = cards.OrderBy(x => UnityEngine.Random.value).ToList();
    }

    void ShuffleCardsIntoDeck(List<string> newCards)
    {
        // TODO implement ShuffleCardsIntoDeck of deck
        throw new NotImplementedException();
    }
}
