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
    
    const int maxHP = 10;
    const int maxMana = 10;
    const int maxHandSize = 5;

    const int manaPerRound = 2;
    // ================================

    MainUI uiManager;
    BoardUI boardUI;

    HumanPlayer player;
    AIPlayer opponent;

    GamePhases currentPhase;

    private void Awake()
    {
        uiManager = FindObjectOfType<MainUI>();
        boardUI = FindObjectOfType<BoardUI>();
    }

    private void Start()
    {
        InitializeGame();
    }

    void InitializeGame()
    {
        player = new HumanPlayer(testingDeck, maxHP, maxHandSize, maxMana);
        opponent = new AIPlayer(testingDeck, maxHP);

        // Draw 2 extra cards for the game setup for the human player
        player.DrawCardFromDeck();
        player.DrawCardFromDeck();

        // Set the initiative
        if (Random.value < 0.5f)
        {
            player.HasInitiative = true;
        }
        else
        {
            opponent.HasInitiative = true;
        }

        SetPhase(GamePhases.Upkeep_Phase);
        ExecutePhaseProcess(currentPhase);

        // Prepare the UI
        uiManager.HitPoints.text = maxHP.ToString();
    }

    public void onNextPhaseClick()
    {
        uiManager.EndPhase.enabled = false;
        SetPhase(currentPhase.NextPhase());
        ExecutePhaseProcess(currentPhase);
        uiManager.EndPhase.enabled = true;
    }

    void SwapInitiative()
    {
        if (player.HasInitiative)
        {
            player.HasInitiative = false;
            opponent.HasInitiative = true;
            boardUI.MoveInitiativeToPlayer(false);
        }
        else
        {
            player.HasInitiative = true;
            opponent.HasInitiative = false;
            boardUI.MoveInitiativeToPlayer(true);
        }
    }

    void SetPhase(GamePhases PhaseToSet)
    {
        currentPhase = PhaseToSet;
        uiManager.Phase.text = currentPhase.GetLabel();
    }

    void ExecutePhaseProcess(GamePhases currentPhase)
    {
        switch (currentPhase)
        {
            case GamePhases.Upkeep_Phase:
                ExecuteUpkeepProcess();
                break;
            case GamePhases.Standard_Phase:
                break;
            case GamePhases.Move_Phase:
                break;
            case GamePhases.Combat_Phase:
                break;
            case GamePhases.End_Phase:
                break;
        }
    }

    void ExecuteUpkeepProcess()
    {
        player.DrawCardFromDeck();
        player.GainMana(manaPerRound);
        SwapInitiative();
    }
}
