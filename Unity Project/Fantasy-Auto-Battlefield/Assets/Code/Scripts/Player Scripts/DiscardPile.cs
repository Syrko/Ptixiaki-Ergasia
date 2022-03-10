using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The <c>DiscardPile</c> class contians the cards owned by the human player that are discarded (e.g. overdrawn or played cards)
/// </summary>
public class DiscardPile : MonoBehaviour
{
    List<string> cards;

    private void Awake()
    {
        cards = new List<string>();
    }

    public List<string> Cards { get => cards; }

    public void AddCard(string card)
    {
        cards.Add(card);
    }

    public void EmptyDiscardPile()
    {
        cards.Clear();
    }
}
