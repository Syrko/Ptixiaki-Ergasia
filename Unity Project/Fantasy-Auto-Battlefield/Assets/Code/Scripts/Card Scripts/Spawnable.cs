using System;
using System.Collections.Generic;
using UnityEngine;

public class Spawnable : Card
{
    string cardName;
    int cardCost;
    string cardText;
    CardType cardType;
    int attack;
    int defense;
    int maxHitPoints;
    int currentHP;
    HexPattern attackPattern;
    Sprite cardImage;
    Material cardMaterial;

    public string CardName { get => cardName; set => cardName = value; }
    public int CardCost { get => cardCost; set => cardCost = value; }
    public string CardText { get => cardText; set => cardText = value; }
    public CardType CardType { get => cardType; set => cardType = value; }
    public int Attack1 { get => attack; set => attack = value; }
    public int Defense { get => defense; set => defense = value; }
    public int MaxHitPoints { get => maxHitPoints; set => maxHitPoints = value; }
    public int CurrentHP { get => currentHP; set => currentHP = value; }
    public HexPattern AttackPattern { get => attackPattern; set => attackPattern = value; }
    public Sprite CardImage { get => cardImage; set => cardImage = value; }
    public Material CardMaterial { get => cardMaterial; set => cardMaterial = value; }

    public void InitializePawn(string unitName)
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

    void UpdateUI()
    {
        PawnStats ui = transform.GetComponentInParent<PawnStats>();
        ui.AttackText.text = attack.ToString();
        ui.DefenseText.text = defense.ToString();
        ui.HitpointsText.text = CurrentHP.ToString();
    }

    void Attack()
    {
        // TODO implement
        throw new NotImplementedException();
    }

    void Die()
    {
        // TODO implement
        throw new NotImplementedException();
    }
}
