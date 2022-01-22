using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // ================================
    // Testing Variables
    // --------------------------------
    List<string> testingDeck = new List<string> { CardCatalog.Soldier, CardCatalog.Soldier, CardCatalog.Gate, CardCatalog.Soldier, CardCatalog.Soldier, CardCatalog.Gate };
    int maxHP = 10;
    int maxMana = 10;
    int maxHandSize = 5;
    // ================================

    HumanPlayer player;
    HumanPlayer opponent; // TODO change type to AIPlayer

    GamePhases currentPhase;

    private void Start()
    {
        Initialize();
    }

    void Initialize()
    {
        player = new HumanPlayer(testingDeck, maxHP, maxHandSize, maxMana);
        opponent = new HumanPlayer(testingDeck, maxHP, maxHandSize, maxMana);

        currentPhase = GamePhases.Upkeep_Phase;

        player.DrawCardFromDeck();
        player.DrawCardFromDeck();
        player.DrawCardFromDeck();
    }

    void SwapInitiative()
    {

    }
}
