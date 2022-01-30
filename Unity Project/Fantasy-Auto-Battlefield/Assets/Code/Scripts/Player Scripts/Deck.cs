using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour
{
    List<string> cards;
    BoardUI boardUI;
    DiscardPile discardPile;

    private void Awake()
    {
        boardUI = GameObject.FindObjectOfType<BoardUI>();
    }

    private void Start()
    {
        boardUI.PlayerDeckCounter.text = cards.Count.ToString(); // TODO change update ui
        ShuffleDeck();
    }

    public void AssignDeckList(List<string> deck)
    {
        cards = deck;
    }

    public string DrawCard()
    {
        // Update the deck counter in the UI and shuffle the discard pile if the deck is empty
        int newDeckCount = int.Parse(boardUI.PlayerDeckCounter.text) - 1;
        if (newDeckCount == 0)
        {
            ShuffleDiscardPileIntoDeck();
            newDeckCount = cards.Count;
        }
        boardUI.PlayerDeckCounter.text = newDeckCount.ToString();
        //---------------------------------

        string drawnCard = cards[0];
        cards.RemoveAt(0);

        return drawnCard;
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
