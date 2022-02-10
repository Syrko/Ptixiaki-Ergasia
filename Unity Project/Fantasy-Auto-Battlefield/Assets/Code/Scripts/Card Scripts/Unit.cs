using System;
using UnityEngine;

public class Unit : Spawnable
{
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

    public void Move()
    {
        // TODO implement Move of units
        throw new NotImplementedException();
    }

    
    private void OnMouseDown()
    {
        SubjectUI.Notify(this.gameObject, new UIEvent(EventUICodes.CARD_INFO_CHANGED));
    }
}
