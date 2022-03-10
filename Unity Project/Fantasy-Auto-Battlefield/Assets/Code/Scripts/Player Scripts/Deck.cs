using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The <c>Deck</c> class contains the cards used by the human player in the game.
/// It gets refilled when it is empty.
/// </summary>
public class Deck : MonoBehaviour
{
    [SerializeField]
    DiscardPile discardPile;

    List<string> cards;

    private void Start()
    {
        SubjectUI.Notify(this.gameObject, new UIEvent(EventUICodes.DECK_COUNTER_CHANGED, cards.Count.ToString()));
        ShuffleDeck();
    }

    /// <summary>
    /// Imports the cards with which the human player will play.
    /// </summary>
    public void AssignDeckList(List<string> deck)
    {
        cards = deck;
        SubjectUI.Notify(this.gameObject, new UIEvent(EventUICodes.DECK_COUNTER_CHANGED, cards.Count.ToString()));
    }

    /// <summary>
    /// If the deck is empty, it refills it with the <c>DiscardPile</c>.
    /// Then it removes the top card, and return its name.
    /// </summary>
    /// <returns>The name of the card drawn</returns>
    public string DrawCard()
    {
        // If the deck is empty shuffle the discard into it
        if (cards.Count == 0)
        {
            ShuffleCardsIntoDeck(discardPile.Cards);
            discardPile.EmptyDiscardPile();
        }

        // Draw the card
        string drawnCard = cards[0];
        cards.RemoveAt(0);

        // Update the UI
        SubjectUI.Notify(this.gameObject, new UIEvent(EventUICodes.DECK_COUNTER_CHANGED, cards.Count.ToString()));

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
        }
        ShuffleDeck();
    }
}
