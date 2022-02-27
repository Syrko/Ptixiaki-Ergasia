using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

// TODO GENERAL: Write many many comments
public partial class GameManager : MonoBehaviour
{
    // ================================
    // Testing Variables
    // TODO remove testing variables
    // --------------------------------
    List<string> testingDeck = new List<string> 
    { CardCatalogue.Soldier.CardName, CardCatalogue.Crabby.CardName, 
        CardCatalogue.Mad_Slug.CardName, CardCatalogue.Yeti.CardName, 
        CardCatalogue.Egg_Thief.CardName, CardCatalogue.Summoner.CardName, 
        CardCatalogue.The_Seventh_Demon.CardName, CardCatalogue.Ghoul.CardName, 
        CardCatalogue.Egg.CardName, CardCatalogue.Healing_Sheep.CardName, 
        CardCatalogue.Little_Imp.CardName, CardCatalogue.Attack_Bot.CardName, 
        CardCatalogue.Fishercat.CardName, CardCatalogue.Gate.CardName, 
        CardCatalogue.Magical_Well.CardName, CardCatalogue.Guard_Tower.CardName,
        CardCatalogue.Cursed_Ruins.CardName, CardCatalogue.Honey_Strength.CardName,
        CardCatalogue.Magical_Conduit.CardName, CardCatalogue.Disintegrate.CardName,
        CardCatalogue.Cataclysm.CardName, CardCatalogue.Revitalize.CardName,
        CardCatalogue.Intoxicate.CardName, CardCatalogue.Mask_Of_Control.CardName,
        CardCatalogue.Knowledge_Is_Power.CardName, CardCatalogue.The_Great_Golem.CardName };
    public List<string> TestingDeck { get => testingDeck; }
    // ================================

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
    [Header("Players")]
    [SerializeField]
    HumanPlayer player;
    [SerializeField]
    AIPlayer opponent;
    [Space(10f)]

    [Header("Result UI")]
    [SerializeField]
    TextMeshProUGUI resultLabel;
    [SerializeField]
    GameObject resultPanel;
    [SerializeField]
    Button exitButton;

    BoardGenerator boardGenerator;
    GamePhases currentPhase;

    private int gameTurnsPlayed = 1;

    public int GameTurnsPlayed { get => gameTurnsPlayed; }
    public int MaxHP { get => maxHP; }
    public int MaxMana { get => maxMana; }
    public int MaxHandSize { get => maxHandSize; }
    public GameObject[,] Board { get => boardGenerator.Board; }
    public int BoardWidth { get=> boardGenerator.BoardWidth; }
    public int BoardDepth { get=> boardGenerator.BoardDepth; }
    public GamePhases CurrentPhase { get => currentPhase; }

    private void Awake()
    {
        boardGenerator = FindObjectOfType<BoardGenerator>();
        Spawnable.explosionFX = Resources.Load<GameObject>("Explosion"); // Initialize the explosion effects for the attacks of the spawnables
        HexTile.TerrainEffectFX = Resources.Load<GameObject>("TerrainEffect");
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
            opponent.HasInitiative = false;
        }
        else
        {
            opponent.HasInitiative = true;
            player.HasInitiative = false;
        }

        // Execute the first Upkeep Phase
        SetPhase(GamePhases.Upkeep_Phase);
        ExecutePhaseProcess(GamePhases.Upkeep_Phase);

        // Prepare the UI
        SubjectUI.Notify(this.gameObject, new UIEvent(EventUICodes.PLAYER_HP_CHANGED, MaxHP.ToString()));
        SubjectUI.Notify(this.gameObject, new UIEvent(EventUICodes.DISABLE_PLAY_BUTTON));
    }

    void ExecutePhaseProcess(GamePhases currentPhase)
    {
        GameLog.Log(this.gameObject, new LogEvent(LogEventCode.PhaseChanged, currentPhase.GetLabel()));
        switch (currentPhase)
        {
            case GamePhases.Upkeep_Phase:
                ExecuteUpkeepProcess();
                break;
            case GamePhases.Standard_Phase:
                ExecuteStandardPhase();
                opponent.PlayAICards();
                break;
            case GamePhases.Move_Phase:
                ExecuteMovePhase();
                break;
            case GamePhases.Combat_Phase:
                ExecuteCombatPhase();
                break;
            case GamePhases.End_Phase:
                ExecuteEndPhase();
                break;
        }
    }

    void ExecuteUpkeepProcess()
    {
        gameTurnsPlayed++;
        player.DrawCardFromDeck();
        player.GainMana(manaPerRound);
        SwapInitiative();
    }

    void ExecuteStandardPhase()
    {
        player.DetermineFrontLine(Board);
        if (player.Hand.SelectedCardIndex != Hand.NO_CARD_SELECTED)
        {
            SubjectUI.Notify(this.gameObject, new UIEvent(EventUICodes.ENABLE_PLAY_BUTTON));
        }
    }
    
    void ExecuteMovePhase()
    {
        SubjectUI.Notify(this.gameObject, new UIEvent(EventUICodes.DISABLE_PLAY_BUTTON));
        SubjectUI.Notify(this.gameObject, new UIEvent(EventUICodes.DISABLE_END_PHASE_BUTTON));
        MoveAllUnits();
        SubjectUI.Notify(this.gameObject, new UIEvent(EventUICodes.ENABLE_END_PHASE_BUTTON));
    }

    void ExecuteCombatPhase()
    {
        AllUnitsAttack();
    }

    void ExecuteEndPhase()
    {
        ExecuteTerrainEffects();
    }

    public void EndGame()
    {
        if (player.CurrentHP == 0 && opponent.CurrentHP == 0)
        {
            // Draw
            resultLabel.text = "Draw";
            resultLabel.color = Color.white;
            resultLabel.gameObject.SetActive(true);
            resultPanel.SetActive(true);
            exitButton.gameObject.SetActive(true);
        }
        else if (opponent.CurrentHP == 0)
        {
            // Player victory
            resultLabel.text = "Victory";
            resultLabel.color = Color.green;
            resultLabel.gameObject.SetActive(true);
            resultPanel.SetActive(true);
            exitButton.gameObject.SetActive(true);
        }
        else if (player.CurrentHP == 0)
        {
            // AI victory
            resultLabel.text = "Defeat";
            resultLabel.color = Color.red;
            resultLabel.gameObject.SetActive(true);
            resultPanel.SetActive(true);
            exitButton.gameObject.SetActive(true);
        }
    }

    public void onExitClick()
    {
        Debug.Log("Exiting...");
        Application.Quit();
    }
}
