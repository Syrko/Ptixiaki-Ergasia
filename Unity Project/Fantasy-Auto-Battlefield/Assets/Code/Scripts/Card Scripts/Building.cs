using System;
using UnityEngine;

public class Building : Spawnable
{
    public void InitializeBuildingPawn(string buildingName)
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

        cardMaterial = Resources.Load<Material>("Cards/Buildings/" + buildingName + "/" + buildingName);
        transform.Find("Image").GetComponent<MeshRenderer>().material = cardMaterial;

        UpdatePawnUI();
    }

    private void OnMouseDown()
    {
        FindObjectOfType<MainUI>().UpdateCardInfo(this);
    }
}
