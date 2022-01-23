using System;
using System.Collections.Generic;
using UnityEngine;

public class DiscardPile
{
    List<string> cards;

    public DiscardPile()
    {
        cards = new List<string>();
    }

    public List<string> Cards { get => cards; }
}
