using System;
using UnityEngine;

public class Building : Spawnable
{
    public void InitializeBuildingPawn(string buildingName)
    {
        BuildingCardData data = Resources.Load<BuildingCardData>("Cards/Buildings/" + buildingName + "/" + buildingName);
        cardName = data.CardName;
        cardCost = data.CardCost;
        cardText = data.CardText;
        cardType = data.CardType;
        attack = data.Attack;
        defense = data.Defense;
        maxHitPoints = data.MaxHitPoints;
        currentHP = maxHitPoints;
        attackPattern = data.AttackPattern;
        cardImage = data.CardImage;

        cardMaterial = Resources.Load<Material>("Cards/Buildings/" + buildingName + "/" + buildingName);
        transform.Find("Image").GetComponent<MeshRenderer>().material = cardMaterial;

        UpdateUI();
    }
}
