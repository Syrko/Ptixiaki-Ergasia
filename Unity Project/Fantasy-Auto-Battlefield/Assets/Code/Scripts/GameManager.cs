using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO GENERAL: Write many many comments
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

    UIManager uiManager;

    HumanPlayer player;
    //HumanPlayer opponent; // TODO change type to AIPlayer

    GamePhases currentPhase;

    private void Awake()
    {
        uiManager = FindObjectOfType<UIManager>();
    }

    private void Start()
    {
        InitializeGame();
    }

    void InitializeGame()
    {
        player = new HumanPlayer(testingDeck, maxHP, maxHandSize, maxMana);
        //opponent = new HumanPlayer(testingDeck, maxHP, maxHandSize, maxMana);

        SetPhase(GamePhases.Combat_Phase);

        player.DrawCardFromDeck();
        player.DrawCardFromDeck();
        player.DrawCardFromDeck();
    }

    void SwapInitiative()
    {

    }

    void SetPhase(GamePhases PhaseToSet)
    {
        currentPhase = PhaseToSet;
        uiManager.Phase.text = currentPhase.GetLabel();
    }
}
