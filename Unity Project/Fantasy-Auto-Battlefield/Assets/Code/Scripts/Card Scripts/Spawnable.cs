using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawnable : Card
{
    public static GameObject explosionFX;
    public static int DamageAmountForBase = 1;
    public static float spawnDespawnTime = 1f;
    public static float spawnDespawnDisplacement = 0.75f;

    PawnStats pawnUI;

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

    private void Awake()
    {
        pawnUI = transform.GetComponentInParent<PawnStats>();
    }

    private void Start()
    {
        StartCoroutine(Spawn(spawnDespawnTime, transform.position));
    }

    protected void InitializePawnUI()
    {
        pawnUI.AttackText.text = attack.ToString();
        pawnUI.DefenseText.text = defense.ToString();
        pawnUI.HitpointsText.text = CurrentHP.ToString();

        ColorPawn();
    }

    protected void UpdatePawnUI()
    {
        pawnUI.AttackText.text = attack.ToString();
        pawnUI.AttackText.color = DetermineValueColor(originalAttack, attack);

        pawnUI.DefenseText.text = defense.ToString();
        pawnUI.DefenseText.color = DetermineValueColor(originalDefense, defense);

        pawnUI.HitpointsText.text = CurrentHP.ToString();
        pawnUI.HitpointsText.color = DetermineValueColor(maxHitPoints, currentHP);
    }

    public void TakeDamage(int amount)
    {
        currentHP -= amount;
        if (currentHP <= 0)
        {
            currentHP = 0;
            UpdatePawnUI();
            this.Die();
        }
        UpdatePawnUI();
    }

    public void HealDamage(int amount)
    {
        currentHP += amount;
        if (currentHP > maxHitPoints)
        {
            currentHP = maxHitPoints;
        }
        UpdatePawnUI();
    }

    public void IncreaseAttack(int amount)
    {
        attack += amount;
        UpdatePawnUI();
    }

    public void DecreaseAttack(int amount)
    {
        attack -= amount;
        if (attack < 0)
        {
            attack = 0;
        }
        UpdatePawnUI();
    }

    public void IncreaseDefense(int amount)
    {
        defense += amount;
        UpdatePawnUI();
    }

    public void DecreaseDefense(int amount)
    {
        defense -= amount;
        if (defense < 0)
        {
            defense = 0;
        }
        UpdatePawnUI();
    }

    public void Attack(GameObject[,] board, Vector2Int attackerPos)
    {
        List<Vector2Int> targets = DetermineTargets(board, attackerPos);
        bool attackedBaseThisTurn = false; // flag so multi-hex targeting attacks do not attack the base multiple times

        foreach (var target in targets)
        {
            HexTile targetHex = board[target.x, target.y].GetComponent<HexTile>();
            
            if (targetHex.OccupiedBy != null)
            {
                // Attack a pawn, if there is one
                Spawnable targetPawn = targetHex.OccupiedBy.GetComponent<Spawnable>();
                if (targetPawn.owner != this.owner)
                {
                    AttackFX(target.x, target.y);
                    int damage = CalculateDamage(this, targetPawn);
                    targetPawn.TakeDamage(damage);
                }
            }

            // And attack the base if this pawn is in front of one
            if (targetHex.TileType == TileType.Base)
            {
                if (attackedBaseThisTurn)
                {
                    break;
                }
                else
                {
                    attackedBaseThisTurn = true;
                }

                // If a human controlled pawn is in front of the AI base
                if (this.owner is HumanPlayer && target.x == board.GetLength(0) - 1)
                {
                    FindObjectOfType<AIPlayer>().TakeDamage(DamageAmountForBase);
                    AttackFX(target.x, target.y);
                }
                // If an AI controlled pawn is in front of the human base
                else if (this.owner is AIPlayer && target.x == 0)
                {
                    FindObjectOfType<HumanPlayer>().TakeDamage(DamageAmountForBase);
                    AttackFX(target.x, target.y);
                }
            }
        }
    }

    public void Die()
    {
        foreach (Renderer renderer in this.transform.GetComponentsInChildren<Renderer>())
        {
            renderer.material.color = Color.gray;
        }
        StartCoroutine(Despawn(spawnDespawnTime, transform.position));
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

    public void ColorPawn()
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

    private void AttackFX(int x, int y)
    {
        Vector3 pos = BoardManager.TranslateCoordinates(y, x, transform.position.y);
        GameObject temp = Instantiate(explosionFX, pos, Quaternion.identity);
    }

    private Color DetermineValueColor(int originalValue, int currentValue)
    {
        if (originalValue > currentValue)
        {
            return Color.red;
        }
        else if (originalValue < currentValue)
        {
            return Color.green;
        }
        else
        {
            return Color.white;
        }
    }

    private IEnumerator Despawn(float time, Vector3 currentPos)
    {
        SubjectUI.Notify(this.gameObject, new UIEvent(EventUICodes.DISABLE_END_PHASE_BUTTON));
        yield return new WaitForSeconds(2f);

        Vector3 startingPos = currentPos;
        Vector3 finalPos = new Vector3(currentPos.x, currentPos.y - spawnDespawnDisplacement, currentPos.z);

        
        float elapsedTime = 0;

        while (elapsedTime < time)
        {
            transform.position = Vector3.Lerp(startingPos, finalPos, (elapsedTime / time));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        SubjectUI.Notify(this.gameObject, new UIEvent(EventUICodes.ENABLE_END_PHASE_BUTTON));
        Destroy(this.gameObject);
    }

    private IEnumerator Spawn(float time, Vector3 currentPos)
    {
        SubjectUI.Notify(this.gameObject, new UIEvent(EventUICodes.DISABLE_END_PHASE_BUTTON));
        Vector3 startingPos = new Vector3(currentPos.x, currentPos.y - spawnDespawnDisplacement, currentPos.z);
        Vector3 finalPos = currentPos;


        float elapsedTime = 0;

        while (elapsedTime < time)
        {
            transform.position = Vector3.Lerp(startingPos, finalPos, (elapsedTime / time));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        SubjectUI.Notify(this.gameObject, new UIEvent(EventUICodes.ENABLE_END_PHASE_BUTTON));
    }
}
