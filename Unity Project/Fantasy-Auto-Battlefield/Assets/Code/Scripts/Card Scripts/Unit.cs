using System;
using UnityEngine;

public class Unit : Spawnable
{
    public void InitializeUnitPawn(string unitName)
    {
        UnitCardData data = Resources.Load<UnitCardData>("Cards/Units/" + unitName + "/" + unitName);
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

        cardMaterial = Resources.Load<Material>("Cards/Units/" + unitName + "/" + unitName);
        transform.Find("Image").GetComponent<MeshRenderer>().material = cardMaterial;

        UpdateUI();
    }

    void Move()
    {
        // TODO implement
        throw new NotImplementedException();
    }
}
