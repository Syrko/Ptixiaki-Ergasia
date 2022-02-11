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
    protected HexPatternCodes attackPattern;
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
    public HexPatternCodes AttackPattern { get => attackPattern; set => attackPattern = value; }
    public Sprite CardImage { get => cardImage; set => cardImage = value; }
    public Material CardMaterial { get => cardMaterial; set => cardMaterial = value; }

    protected void InitializePawnUI()
    {
        PawnStats ui = transform.GetComponentInParent<PawnStats>();
        ui.AttackText.text = attack.ToString();
        ui.DefenseText.text = defense.ToString();
        ui.HitpointsText.text = CurrentHP.ToString();

        ColorPawn();
    }

    protected void TakeDamage(int amount)
    {
        currentHP -= amount;
        if (currentHP <= 0)
        {
            currentHP = 0;
            Debug.Log("Death"); // TODO remove
        }
        SubjectUI.Notify(this.gameObject, new UIEvent(EventUICodes.SPAWNABLE_HP_CHANGE, currentHP.ToString()));
    }

    protected void HealDamage(int amount)
    {
        throw new NotImplementedException();
    }

    public void Attack(GameObject[,] board, Vector2Int attackerPos)
    {
        List<Vector2Int> targets = DetermineTargets(board, attackerPos);
        foreach (var target in targets)
        {
            HexTile hex = board[target.x, target.y].GetComponent<HexTile>();
            if (hex.OccupiedBy != null)
            {
                Spawnable targetPawn = hex.OccupiedBy.GetComponent<Spawnable>();
                if (targetPawn.owner != this.owner)
                {
                    int damage = CalculateDamage(this, targetPawn);
                    targetPawn.TakeDamage(damage);
                }
            }
        }
    }

    void Die()
    {
        // TODO implement Die of spawnable
        throw new NotImplementedException();
    }

    private int CalculateDamage(Spawnable attacker, Spawnable target)
    {
        int attack = attacker.attack - target.defense;
        if (attack <= 0)
        {
            attack = 1;
        }
        return attack;
    }

    private void ColorPawn()
    {
        Component[] borders = transform.Find("Border").gameObject.GetComponentsInChildren(typeof(Renderer));
        foreach (Component renderer in borders)
        {
            if (owner is HumanPlayer)
            {
                ((Renderer)renderer).material.color = Color.blue;
            }
            else if (owner is AIPlayer)
            {
                ((Renderer)renderer).material.color = Color.red;
            }
        }
    }


    private bool areCoordsInsideOfBounds(GameObject[,] board, Vector2Int depthWidth)
    {
        if (depthWidth.x >= 0 && depthWidth.x < board.GetLength(0))
        {
            if (depthWidth.y >= 0 && depthWidth.y < board.GetLength(1))
            {
                return true;
            }
        }
        return false;
    }

    private List<Vector2Int> DetermineTargets(GameObject[,] board, Vector2Int attackerPos)
    {
        List<Vector2Int> targets = new List<Vector2Int>();

        string[] hexPatternTargets = HexPattern.GetHexPatternInStrings(attackPattern);

        foreach (string hexPatternTarget in hexPatternTargets)
        {
            Vector2Int possibleTarget;
            // To account for the difference in perspective of the board
            if (this.owner is HumanPlayer)
            {
                switch (hexPatternTarget)
                {
                    case "FL":
                        possibleTarget = new Vector2Int(attackerPos.x + 1, attackerPos.y - 1);
                        if (areCoordsInsideOfBounds(board, possibleTarget))
                        {
                            targets.Add(possibleTarget);
                        }
                        break;
                    case "FF":
                        possibleTarget = new Vector2Int(attackerPos.x + 1, attackerPos.y);
                        if (areCoordsInsideOfBounds(board, possibleTarget))
                        {
                            targets.Add(possibleTarget);
                        }
                        break;
                    case "FR":
                        possibleTarget = new Vector2Int(attackerPos.x + 1, attackerPos.y + 1);
                        if (areCoordsInsideOfBounds(board, possibleTarget))
                        {
                            targets.Add(possibleTarget);
                        }
                        break;
                    case "BL":
                        possibleTarget = new Vector2Int(attackerPos.x, attackerPos.y - 1);
                        if (areCoordsInsideOfBounds(board, possibleTarget))
                        {
                            targets.Add(possibleTarget);
                        }
                        break;
                    case "BB":
                        possibleTarget = new Vector2Int(attackerPos.x - 1, attackerPos.y);
                        if (areCoordsInsideOfBounds(board, possibleTarget))
                        {
                            targets.Add(possibleTarget);
                        }
                        break;
                    case "BR":
                        possibleTarget = new Vector2Int(attackerPos.x, attackerPos.y + 1);
                        if (areCoordsInsideOfBounds(board, possibleTarget))
                        {
                            targets.Add(possibleTarget);
                        }
                        break;
                }
            } 
            else if (this.owner is AIPlayer)
            {
                switch (hexPatternTarget)
                {
                    case "BR":
                        possibleTarget = new Vector2Int(attackerPos.x + 1, attackerPos.y - 1);
                        if (areCoordsInsideOfBounds(board, possibleTarget))
                        {
                            targets.Add(possibleTarget);
                        }
                        break;
                    case "BB":
                        possibleTarget = new Vector2Int(attackerPos.x + 1, attackerPos.y);
                        if (areCoordsInsideOfBounds(board, possibleTarget))
                        {
                            targets.Add(possibleTarget);
                        }
                        break;
                    case "BL":
                        possibleTarget = new Vector2Int(attackerPos.x + 1, attackerPos.y + 1);
                        if (areCoordsInsideOfBounds(board, possibleTarget))
                        {
                            targets.Add(possibleTarget);
                        }
                        break;
                    case "FR":
                        possibleTarget = new Vector2Int(attackerPos.x, attackerPos.y - 1);
                        if (areCoordsInsideOfBounds(board, possibleTarget))
                        {
                            targets.Add(possibleTarget);
                        }
                        break;
                    case "FF":
                        possibleTarget = new Vector2Int(attackerPos.x - 1, attackerPos.y);
                        if (areCoordsInsideOfBounds(board, possibleTarget))
                        {
                            targets.Add(possibleTarget);
                        }
                        break;
                    case "FL":
                        possibleTarget = new Vector2Int(attackerPos.x, attackerPos.y + 1);
                        if (areCoordsInsideOfBounds(board, possibleTarget))
                        {
                            targets.Add(possibleTarget);
                        }
                        break;
                }
            }
        }
        return targets;
    }
}
