using System;
using UnityEngine;

public class Unit : Spawnable
{
    public void InitializeUnitPawn(string unitName)
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

        cardMaterial = Resources.Load<Material>("Cards/Units/" + unitName + "/" + unitName);
        transform.Find("Image").GetComponent<MeshRenderer>().material = cardMaterial;

        UpdatePawnUI();
    }

    void Move()
    {
        // TODO implement Move of units
        throw new NotImplementedException();
    }

    
    private void OnMouseDown()
    {
        SubjectUI.Notify(this.gameObject, new EventUI(EventUICodes.CARD_INFO_CHANGED));
    }
}
