using System;
using System.Collections.Generic;
using UnityEngine;

public class Hand
{
    List<string> cards;

    UIManager ui;

    public Hand()
    {
        cards = new List<string>();
        ui = GameObject.FindObjectOfType<UIManager>();
    }

    void ToggleHand()
    { 
        // TODO implement toggle hand
        throw new NotImplementedException();
    }

    public void AddCard(string card)
    {
        cards.Add(card);
        UpdateHandUI();
    }

    private void UpdateHandUI()
    {
        for (int i = 0; i < cards.Count; i++)
        {
            ui.ShowCardInHand(i, cards[i]);
        }
    }
}
