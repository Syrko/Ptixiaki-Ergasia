using System;
using System.Collections.Generic;
using UnityEngine;

public class Spawnable : Card
{
    protected string cardName;
    protected int cardCost;
    protected int originalCardCost;
    protected Player owner;
    protected Player originalOwner;
    protected string cardText;
    protected CardType cardType;
    protected int attack;
    protected int originalAttack;
    protected int defense;
    protected int originalDefense;
    protected int maxHitPoints;
    protected int currentHP;
    protected HexPattern attackPattern;
    protected Sprite cardImage;
    protected Material cardMaterial;

    public string CardName { get => cardName; set => cardName = value; }
    public int CardCost { get => cardCost; set => cardCost = value; }
    public int OriginalCardCost { get => originalCardCost; set => originalCardCost = value; }
    public Player Owner { get => owner; set => owner = value; }
    public Player OriginalOwner { get => originalOwner; set => originalOwner = value; }
    public string CardText { get => cardText; set => cardText = value; }
    public CardType CardType { get => cardType; set => cardType = value; }
    public int AttackValue { get => attack; set => attack = value; }
    public int OriginalAttackValue { get => originalAttack; set => originalAttack = value; }
    public int Defense { get => defense; set => defense = value; }
    public int OriginalDefense { get => originalDefense; set => originalDefense = value; }
    public int MaxHitPoints { get => maxHitPoints; set => maxHitPoints = value; }
    public int CurrentHP { get => currentHP; set => currentHP = value; }
    public HexPattern AttackPattern { get => attackPattern; set => attackPattern = value; }
    public Sprite CardImage { get => cardImage; set => cardImage = value; }
    public Material CardMaterial { get => cardMaterial; set => cardMaterial = value; }

    protected void UpdateUI()
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
