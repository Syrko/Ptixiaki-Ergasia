using System;
using UnityEngine;

public class Building : Spawnable
{
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

    private void OnMouseDown()
    {
        SubjectUI.Notify(this.gameObject, new UIEvent(EventUICodes.CARD_INFO_CHANGED));
    }
}
