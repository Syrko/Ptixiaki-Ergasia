using System;
using UnityEngine;

/// <summary>
/// <c>Building</c> is monobehaviour inheriting from the Spawnable class and is attached to 
/// the gameobjects representing a Building card
/// </summary>
public class Building : Spawnable
{
    /// <summary>
    /// Initializes a new Building with data from the appropriate Scriptable Object.
    /// Should be called after attaching the script to a gameobject.
    /// </summary>
    /// <param name="buildingName">The name of the Building you are creating.</param>
    /// <param name="forPlayer">Set as true, if the created pawn is owned by the human player.</param>
    public void InitializeBuildingPawn(string buildingName, bool forPlayer)
    {
        BuildingCardData data = BuildingCardData.GetBuildingDataFromName(buildingName);
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

        cardMaterial = Resources.Load<Material>("Cards/Buildings/" + buildingName + "/" + buildingName);
        transform.Find("Image").GetComponent<MeshRenderer>().material = cardMaterial;

        InitializePawnUI();
    }

    /// <summary>
    /// Update the Card Info UI when clicking on the pawn.
    /// </summary>
    private void OnMouseDown()
    {
        SubjectUI.Notify(this.gameObject, new UIEvent(EventUICodes.CARD_INFO_CHANGED));
    }
}
