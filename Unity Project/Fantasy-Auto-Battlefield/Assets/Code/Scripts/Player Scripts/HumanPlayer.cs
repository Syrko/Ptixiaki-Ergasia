using System;
using System.Collections.Generic;
using UnityEngine;

public class HumanPlayer : Player
{
    Hand hand;
    DiscardPile discardPile;
    int maxHandSize;
    int mana;
    int maxMana;

    MainUI ui;

    public HumanPlayer(List<string> deck, int maxHP, int maxHandSize, int maxMana) : base(deck, maxHP)
    {
        this.hand = new Hand();
        this.discardPile = new DiscardPile();
        this.deck.AssignDiscardPile(this.discardPile);
        this.maxHandSize = maxHandSize;
        this.maxMana = maxMana;
        this.mana = 0;

        ui = GameObject.FindObjectOfType<MainUI>();
    }

    void PayMana(int amount)
    {
        // TODO implement pay mana
        throw new NotImplementedException();
    }

    public void GainMana(int amount)
    {
        mana += amount;
        if(mana > maxMana)
        {
            mana = maxMana;
        }

        ui.Mana.text = mana.ToString();
    }

    public void DrawCardFromDeck()
    {
        string cardName = deck.DrawCard();
        if (hand.CardCount < maxHandSize)
        {
            hand.AddCard(cardName);
        }
        else
        {
            discardPile.AddCard(cardName);
        }
    }
}
