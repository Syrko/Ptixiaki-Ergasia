using System;
using System.Collections.Generic;
using UnityEngine;

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
