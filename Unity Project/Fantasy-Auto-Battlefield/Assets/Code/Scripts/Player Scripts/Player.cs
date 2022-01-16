using System;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    protected Deck deck;
    protected int maxHP;
    protected int hp;
    protected bool hasInitiative;
    protected int frontline;

    public Player(List<string> deck, int maxHP)
    {
        this.deck = new Deck(deck);
        this.maxHP = maxHP;
        this.hp = maxHP;
        this.hasInitiative = false;
        this.frontline = 0;
    }

    protected void ToggleInitiative()
    {
        // TODO implement
        throw new NotImplementedException();
    }

    protected void TakeDamage(int amount)
    {
        // TODO implement
        throw new NotImplementedException();
    }

    protected void HealSelf(int amount)
    {
        // TODO implement
        throw new NotImplementedException();
    }
}
