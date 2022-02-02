using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// TODO GENERAL: Write many many comments
public class GameManager : MonoBehaviour
{
    // ================================
    // Testing Variables
    // --------------------------------
    List<string> testingDeck = new List<string> { CardCatalog.Soldier, CardCatalog.Soldier, CardCatalog.Soldier, CardCatalog.Soldier, CardCatalog.Soldier, CardCatalog.Gate, CardCatalog.Gate, CardCatalog.Gate, CardCatalog.Gate, CardCatalog.Gate};
    public List<string> TestingDeck { get => testingDeck; }

    [Header("Game Parameters")]
    [SerializeField]
    int maxHP = 10;
    [SerializeField]
    int maxMana = 10;
    [SerializeField]
    int maxHandSize = 5;
    [SerializeField]
    int manaPerRound = 2;
    [Space(10f)]
    // ================================
    [SerializeField]
    Player player;
    //AIPlayer opponent; // TODO serialize it

    BoardGenerator boardGenerator;
    GamePhases currentPhase;

    public int MaxHP { get => maxHP; }
    public int MaxMana { get => maxMana; }
    public int MaxHandSize { get => maxHandSize; }
    public GameObject[,] Board { get => boardGenerator.Board; }
    public int BoardWidth { get=> boardGenerator.BoardWidth; }
    public int BoardDepth { get=> boardGenerator.BoardDepth; }

    private void Awake()
    {
        boardGenerator = FindObjectOfType<BoardGenerator>();
    }

    private void Start()
    {
        InitializeGame();
    }

    void InitializeGame()
    {
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
            // TODO remove comment
            //opponent.HasInitiative = true;
        }

        // Execute the first Upkeep Phase
        SetPhase(GamePhases.Upkeep_Phase);
        ExecutePhaseProcess(GamePhases.Upkeep_Phase);

        // Prepare the UI
        SubjectUI.Notify(this.gameObject, new UIEvent(EventUICodes.PLAYER_HP_CHANGED, MaxHP.ToString()));
        SubjectUI.Notify(this.gameObject, new UIEvent(EventUICodes.TOGGLE_PLAY_BUTTON));
    }

    public void onNextPhaseClick()
    {
        SubjectUI.Notify(this.gameObject, new UIEvent(EventUICodes.TOGGLE_END_PHASE_BUTTON));
        SetPhase(currentPhase.NextPhase());
        ExecutePhaseProcess(currentPhase);
        SubjectUI.Notify(this.gameObject, new UIEvent(EventUICodes.TOGGLE_END_PHASE_BUTTON));
    }

    void SwapInitiative()
    {
        if (player.HasInitiative)
        {
            player.HasInitiative = false;
            // TODO remove comment
            //opponent.HasInitiative = true;
            SubjectUI.Notify(this.gameObject, new UIEvent(EventUICodes.INITIATIVE_TOKEN_SWAPPED));
        }
        else
        {
            player.HasInitiative = true;
            // TODO remove comment
            //opponent.HasInitiative = false;
            SubjectUI.Notify(this.gameObject, new UIEvent(EventUICodes.INITIATIVE_TOKEN_SWAPPED));
        }
    }

    void SetPhase(GamePhases PhaseToSet)
    {
        currentPhase = PhaseToSet;
        SubjectUI.Notify(this.gameObject, new UIEvent(EventUICodes.PHASE_CHANGED, currentPhase.GetLabel()));
    }

    void ExecutePhaseProcess(GamePhases currentPhase)
    {
        switch (currentPhase)
        {
            case GamePhases.Upkeep_Phase:
                ExecuteUpkeepProcess();
                break;
            case GamePhases.Standard_Phase:
                ExecuteStandardPhase();
                break;
            case GamePhases.Move_Phase:
                ExecuteMovePhase();
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

    void ExecuteStandardPhase()
    {
        SubjectUI.Notify(this.gameObject, new UIEvent(EventUICodes.TOGGLE_PLAY_BUTTON));
        player.DetermineFrontLine(Board);
    }
    
    void ExecuteMovePhase()
    {
        SubjectUI.Notify(this.gameObject, new UIEvent(EventUICodes.TOGGLE_PLAY_BUTTON));
    }
}
