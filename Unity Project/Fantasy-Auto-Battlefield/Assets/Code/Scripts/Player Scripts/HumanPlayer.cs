using System;
using System.Collections.Generic;
using UnityEngine;

public class HumanPlayer : Player
{
    [SerializeField]
    GameManager gameManager;
    [SerializeField]
    int maxFrontline = 3;
    [SerializeField]
    Hand hand;
    [SerializeField]
    DiscardPile discardPile;

    Deck deck;
    int maxHandSize;

    int maxMana;
    int currentMana;

    public Hand Hand { get { return hand; } }
    public int CurrentMana { get => currentMana; }

    private void Awake()
    {
        deck = FindObjectOfType<Deck>();
        deck.AssignDeckList(gameManager.TestingDeck); // TODO change the method of deck importing
        maxHandSize = gameManager.MaxHandSize;

        maxHP = gameManager.MaxHP;
        currentHP = gameManager.MaxHP;
        maxMana = gameManager.MaxMana;
        currentMana = 0;

        frontline = 0;
        hasInitiative = false;
    }

    public void GainMana(int amount)
    {
        currentMana += amount;
        if (currentMana > maxMana)
        {
            currentMana = maxMana;
        }

        // Update UI
        SubjectUI.Notify(this.gameObject, new UIEvent(EventUICodes.PLAYER_MANA_CHANGED, currentMana.ToString()));
    }

    public void PayMana(int amount)
    {
        currentMana -= amount;
        if (currentMana < 0)
        {
            currentMana += amount;
            throw new Exception("Not enough mana!");
        }

        // Update UI
        SubjectUI.Notify(this.gameObject, new UIEvent(EventUICodes.PLAYER_MANA_CHANGED, currentMana.ToString()));
    }

    public void DrawCardFromDeck()
    {
        string cardName = deck.DrawCard();
        if (hand.CardsInHandCount < maxHandSize)
        {
            hand.AddCard(cardName);
        }
        else
        {
            discardPile.AddCard(cardName);
        }
    }

    public void DetermineFrontLine(GameObject[,] board)
    {
        int tempFrontLine = 0;

        // Checking every row for occupant nad check its owner
        for (int depth = 0; depth < gameManager.BoardDepth; depth++)
        {
            for(int width = 0; width < gameManager.BoardWidth; width++)
            {
                GameObject occupant = board[depth, width].GetComponent<HexTile>().OccupiedBy;
                if (occupant != null)
                {
                    if (occupant.GetComponent<Spawnable>().Owner is HumanPlayer)
                    {
                        if (depth > tempFrontLine)
                        {
                            tempFrontLine = Math.Min(depth, maxFrontline);
                        }
                    }
                }
            }
        }
        frontline = tempFrontLine;
    }
}
