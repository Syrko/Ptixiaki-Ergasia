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

    MainUI mainUI;
    BoardUI boardUI;
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
        mainUI = FindObjectOfType<MainUI>();
        boardUI = FindObjectOfType<BoardUI>();
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
            //opponent.HasInitiative = true;
        }

        // Execute the first Upkeep Phase
        SetPhase(GamePhases.Upkeep_Phase);
        ExecutePhaseProcess(GamePhases.Upkeep_Phase);

        // Prepare the UI
        // TODO change to update ui
        mainUI.HitPoints.text = MaxHP.ToString();
        ToggleButton(mainUI.PlayCard);
    }

    public void onNextPhaseClick()
    {
        ToggleButton(mainUI.EndPhase);
        SetPhase(currentPhase.NextPhase());
        ExecutePhaseProcess(currentPhase);
        ToggleButton(mainUI.EndPhase);
    }

    void SwapInitiative()
    {
        if (player.HasInitiative)
        {
            player.HasInitiative = false;
            //opponent.HasInitiative = true;
            SubjectUI.Notify(this.gameObject, new EventUI(EventUICodes.INITIATIVE_TOKEN_SWAPPED));
        }
        else
        {
            player.HasInitiative = true;
            //opponent.HasInitiative = false;
            SubjectUI.Notify(this.gameObject, new EventUI(EventUICodes.INITIATIVE_TOKEN_SWAPPED));
        }
    }

    void SetPhase(GamePhases PhaseToSet)
    {
        currentPhase = PhaseToSet;
        mainUI.Phase.text = currentPhase.GetLabel();
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
        ToggleButton(mainUI.PlayCard);
        player.DetermineFrontLine(Board);
    }
    
    void ExecuteMovePhase()
    {
        ToggleButton(mainUI.PlayCard);
    }

    void ToggleButton(Button button)
    {
        if (button.enabled)
        {
            button.enabled = false;
            button.GetComponent<Image>().color = Color.gray;
        }
        else
        {
            button.enabled = true;
            button.GetComponent<Image>().color = Color.white;
        }
    }
}
