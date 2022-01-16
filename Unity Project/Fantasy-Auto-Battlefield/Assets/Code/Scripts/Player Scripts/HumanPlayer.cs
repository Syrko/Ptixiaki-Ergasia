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

    public HumanPlayer(List<string> deck, int maxHP, int maxHandSize, int maxMana) : base(deck, maxHP)
    {
        this.hand = new Hand();
        this.discardPile = new DiscardPile();
        this.maxHandSize = maxHP;
        this.maxMana = maxMana;
        this.mana = 0;
    }

    void PayMana(int amount)
    {
        // TODO implement
        throw new NotImplementedException();
    }

    void GainMana(int amount)
    {
        // TODO implement
        throw new NotImplementedException();
    }
}
