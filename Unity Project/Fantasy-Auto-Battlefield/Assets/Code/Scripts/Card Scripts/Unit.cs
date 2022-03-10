using System;
using System.Collections;
using UnityEngine;

/// <summary>
/// <c>Unit</c> is monobehaviour inheriting from the Spawnable class and is attached to 
/// the gameobjects representing a Unit card
/// </summary>
public class Unit : Spawnable
{
    // Duration for the pawns' move animations
    public static readonly float MOVE_DURATION = 1f;


    /// <summary>
    /// Initializes a new Unit with data from the appropriate Scriptable Object.
    /// Should be called after attaching the script to a gameobject.
    /// </summary>
    /// <param name="unitName">The name of the unit you are creating.</param>
    /// <param name="forPlayer">Set as true, if the created pawn is owned by the human player.</param>
    public void InitializeUnitPawn(string unitName, bool forPlayer)
    {
        UnitCardData data = UnitCardData.GetUnitDataFromName(unitName);
        cardName = data.CardName;
        cardCost = data.CardCost;
        originalCardCost = data.CardCost;
        cardText = data.CardText;
        cardType = data.CardType;
        attack = data.Attack;
        originalAttack = data.Attack;
        defense = data.Defense;
        originalDefense = data.Defense;
        maxHitPoints = data.MaxHitPoints;
        currentHP = maxHitPoints;
        attackPattern = data.AttackPattern;
        cardImage = data.CardImage;

        if (forPlayer)
            originalOwner = FindObjectOfType<HumanPlayer>();
        else
            originalOwner = FindObjectOfType<AIPlayer>();
        owner = originalOwner;

        cardMaterial = Resources.Load<Material>("Cards/Units/" + unitName + "/" + unitName);
        transform.Find("Image").GetComponent<MeshRenderer>().material = cardMaterial;

        InitializePawnUI();
    }

    /// <summary>
    /// Move the pawn ahead one hex tile from its current position.
    /// </summary>
    /// <param name="currentPos">The curremt position of the pawn on the array board.</param>
    public void Move(Vector2Int currentPos)
    {
        if (owner is HumanPlayer)
        {
            StartCoroutine(MovePlayerPawn(MOVE_DURATION, currentPos));
        }
        else if (owner is AIPlayer)
        {
            StartCoroutine(MoveAIPawn(MOVE_DURATION, currentPos));
        }
    }

    
    /// <summary>
    /// Update the Card Info UI when clicking on the pawn.
    /// </summary>
    private void OnMouseDown()
    {
        SubjectUI.Notify(this.gameObject, new UIEvent(EventUICodes.CARD_INFO_CHANGED));
    }

    /// <summary>
    /// Coroutine that animates the movement of the pawns owned by the human player.
    /// </summary>
    /// <param name="time">Duration of the animation.</param>
    /// <param name="currentPos">The current position of the pawn on the array board.</param>
    private IEnumerator MovePlayerPawn(float time, Vector2Int currentPos)
    {
        Vector3 startingPos = transform.position;
        Vector3 finalPos = BoardManager.TranslateCoordinates(currentPos.y, currentPos.x + 1, transform.position.y);

        float elapsedTime = 0;

        while (elapsedTime < time)
        {
            transform.position = Vector3.Lerp(startingPos, finalPos, (elapsedTime / time));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }

    /// <summary>
    /// Coroutine that animates the movement of the pawns owned by the AI player.
    /// </summary>
    /// <param name="time">Duration of the animation.</param>
    /// <param name="currentPos">The current position of the pawn on the array board.</param>
    private IEnumerator MoveAIPawn(float time, Vector2Int currentPos)
    {
        Vector3 startingPos = transform.position;
        Vector3 finalPos = BoardManager.TranslateCoordinates(currentPos.y, currentPos.x - 1, transform.position.y);

        float elapsedTime = 0;

        while (elapsedTime < time)
        {
            transform.position = Vector3.Lerp(startingPos, finalPos, (elapsedTime / time));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }
}
