using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// <c>HumanPlayer</c> is a monobeahaviour that inherits from the <c>Player</c> class.
/// It represents the user that plays the game.
/// </summary>
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
    public DiscardPile DiscardPile { get { return discardPile; } }
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

    /// <summary>
    /// The player gains the given amount of mana
    /// </summary>
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

    /// <summary>
    /// The player loses the given amount of mana
    /// </summary>
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

    /// <summary>
    /// The player draws a card from their deck
    /// </summary>
    public void DrawCardFromDeck()
    {
        string cardName = deck.DrawCard();
        if (hand.CardsInHandCount < maxHandSize)
        {
            hand.AddCard(cardName);
            GameLog.Log(this.gameObject, new LogEvent(LogEventCode.CardDrawnHand, cardName));
        }
        else
        {
            discardPile.AddCard(cardName);
            GameLog.Log(this.gameObject, new LogEvent(LogEventCode.CardDrawnDiscard, cardName));
        }
    }

    /// <summary>
    /// Determines the front line of the human player.
    /// It is not included in the shared bahaviour of the <c>Player</c> class, 
    /// to account for the different perspective of the board.
    /// </summary>
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
