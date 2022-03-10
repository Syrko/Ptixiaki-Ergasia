using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexTile : MonoBehaviour
{
    public static int TerrainEffectMagnitude = 1;   // e.g how much will a unit heal on a lake
    public static GameObject TerrainEffectFX;       // VFX for the terrain effects

    static bool targetFlag = false;
    static readonly float displacement = 100f;      // Card displacement when played
    static readonly float duration = 1f;            // Time in seconds over which the displacement of the card takes place
    static readonly Color originalHighlightColor = new Color(0, 1, 0, 0.2f);
    static readonly Color targetedHighlightColor = new Color(0, 0, 1, 0.2f);

    [SerializeField, Tooltip("The data of the hex according to its category (e.g forest)")]
    TileData tileData;

    GameManager gameManager;

    GameObject occupiedBy = null;
    GameObject highlightHex;
    
    TileHeight tileHeight;
    int posX;
    int posY;

    public GameObject OccupiedBy { get => occupiedBy; set => occupiedBy = value; }
    public TileType TileType { get => tileData.TileType; }
    public int PosX { get => posX; }
    public int PosY { get => posY; }

    /// <summary>
    /// Initializes variables and the UI.
    /// </summary>
    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        highlightHex = transform.Find("Highlight").gameObject;
        Highlight(false);
    }

    /// <summary>
    /// Initializes the coordinates of the hex tile.
    /// </summary>
    /// <param name="d">Position of the hex, in relation to the depth of the board</param>
    /// <param name="w">Position of the hex, in relation to the width of the board</param>
    public void InitializeCoords(int d, int w)
    {
        posX = w;
        posY = d;
    }

    /// <summary>
    /// Handles the selection of each hex to display the correct information on the UI
    /// and to start the play-card sequence if the hex was a valid target
    /// </summary>
    private void OnMouseDown()
	{
        SubjectUI.Notify(tileData, new UIEvent(EventUICodes.TILE_INFO_CHANGED));
        if (targetFlag)
        {
            return;
        }
        else
        {
            if (highlightHex.activeSelf)
            {
                CardInHand.cardIsBeingPlayed = true;
                targetFlag = true;
                StartCoroutine(PlayCardSequence());
            }
        }
	}
    
    /// <summary>
    /// Turns the highlight of the hex on or off.
    /// </summary>
    /// <param name="highlightON">Pass true to turn the highlight on</param>
    public void Highlight(bool highlightON)
    {
        highlightHex.SetActive(highlightON);
    }

    /// <summary>
    /// Co-routine that handles the playing of a card from the hand.
    /// </summary>
    IEnumerator PlayCardSequence()
    {
        // Changes the highlight color to display the choice of the player
        highlightHex.GetComponent<Renderer>().material.color = targetedHighlightColor;
        yield return new WaitForSecondsRealtime(0.2f);

        // Play card sequence
        GameObject selectedCard = gameManager.GetSelectedCard();
        if (selectedCard != null)
        {
            GameLog.Log(FindObjectOfType<HumanPlayer>().gameObject, new LogEvent(LogEventCode.CardPlayed, selectedCard.GetComponent<CardInHand>().CardName));

            StartCoroutine(FloatCard(selectedCard));
            highlightHex.GetComponent<Renderer>().material.color = originalHighlightColor;
            gameManager.DeHighlightBoard();
        }
        else
        {
            highlightHex.GetComponent<Renderer>().material.color = originalHighlightColor;
            gameManager.DeHighlightBoard();
        }
    }

    /// <summary>
    /// Co-routine that handles the card animation when played.
    /// </summary>
    IEnumerator FloatCard(GameObject card)
    {
        Vector3 startingPos = card.transform.position;
        Vector3 finalPos = card.transform.position + (card.transform.up * displacement);
        float elapsedTime = 0;

        while (elapsedTime < duration)
        {
            card.transform.position = Vector3.Lerp(startingPos, finalPos, (elapsedTime / duration));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        card.SetActive(false);
        card.transform.position = startingPos;
        if (card.GetComponent<CardInHand>().CardType == CardType.Spell)
        {
            gameManager.PlaySpellCard(card.GetComponent<CardInHand>(), posX, posY);
        }
        else
        {
            gameManager.PlaySpawnableCard(card.GetComponent<CardInHand>(), posX, posY, true);
        }

        targetFlag = false;
    }
}
