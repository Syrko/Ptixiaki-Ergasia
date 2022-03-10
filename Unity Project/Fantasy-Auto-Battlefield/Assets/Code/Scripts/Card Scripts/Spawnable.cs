using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// <c>Spawnable</c> is monobehaviour inheriting from the Card class and is attached to 
/// the gameobjects representing a Unit or a Building card, as the respective <c>Unit</c> and
/// <c>Building</c> scripts inherit from this class.
/// </summary>
public class Spawnable : Card
{
    public static GameObject explosionFX;                   // Particle Effect for the representation of a pawn's attack
    public static int DamageAmountForBase = 1;              // How much damage may a pawn deal to the enemy base
    public static float spawnDespawnTime = 1f;              // How long the despawn animation takes
    public static float spawnDespawnDisplacement = 0.75f;   // The distance that the pawn moves when despawning

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

    /// <summary>
    /// This method initiatializes the pawn's UI.
    /// This method is called by the children classes when initializing themselves.
    /// </summary>
    protected void InitializePawnUI()
    {
        pawnUI.AttackText.text = attack.ToString();
        pawnUI.DefenseText.text = defense.ToString();
        pawnUI.HitpointsText.text = CurrentHP.ToString();

        ColorPawn();
    }

    /// <summary>
    /// This method updates a pawn's UI with data taken from the pawn's <c>Unit</c> or <c>Building</c> script.
    /// </summary>
    protected void UpdatePawnUI()
    {
        pawnUI.AttackText.text = attack.ToString();
        pawnUI.AttackText.color = DetermineValueColor(originalAttack, attack);

        pawnUI.DefenseText.text = defense.ToString();
        pawnUI.DefenseText.color = DetermineValueColor(originalDefense, defense);

        pawnUI.HitpointsText.text = CurrentHP.ToString();
        pawnUI.HitpointsText.color = DetermineValueColor(maxHitPoints, currentHP);
    }

    /// <summary>
    /// The pawn takes an amount of damage.
    /// </summary>
    /// <param name="amount">The value of damage taken</param>
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

    /// <summary>
    /// The pawn heals an amount of hitpoints.
    /// </summary>
    /// <param name="amount">The value of the heal</param>
    public void HealDamage(int amount)
    {
        currentHP += amount;
        if (currentHP > maxHitPoints)
        {
            currentHP = maxHitPoints;
        }
        UpdatePawnUI();
    }

    /// <summary>
    /// Increase the attack of the pawn by the amount given
    /// </summary>
    /// <param name="amount">The amount of attack increase</param>
    public void IncreaseAttack(int amount)
    {
        attack += amount;
        UpdatePawnUI();
    }

    /// <summary>
    /// Decrease the attack of the pawn by the amount given
    /// </summary>
    /// <param name="amount">The amount of attack decrease</param>
    public void DecreaseAttack(int amount)
    {
        attack -= amount;
        if (attack < 0)
        {
            attack = 0;
        }
        UpdatePawnUI();
    }

    /// <summary>
    /// Increase the defense of the pawn by the amount given
    /// </summary>
    /// <param name="amount">The amount of defense increase</param>
    public void IncreaseDefense(int amount)
    {
        defense += amount;
        UpdatePawnUI();
    }

    /// <summary>
    /// Decrease the defense of the pawn by the amount given
    /// </summary>
    /// <param name="amount">The amount of defense decrease</param>
    public void DecreaseDefense(int amount)
    {
        defense -= amount;
        if (defense < 0)
        {
            defense = 0;
        }
        UpdatePawnUI();
    }

    /// <summary>
    /// This method is called when a pawn attacks.
    /// It determines the possible targets of the pawn and then deals damage to the opposing entites (units, building, bases)
    /// </summary>
    /// <param name="board">The board array</param>
    /// <param name="attackerPos">The current position of the pawn on the array</param>
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

    /// <summary>
    /// This method handles the death of a pawn
    /// </summary>
    public void Die()
    {
        foreach (Renderer renderer in this.transform.GetComponentsInChildren<Renderer>())
        {
            renderer.material.color = Color.gray;
        }
        StartCoroutine(Despawn(spawnDespawnTime, transform.position));
    }

    /// <summary>
    /// Method that calculates the damage of that an attacker deals to a defender
    /// </summary>
    /// <param name="attacker">The <c>Spawnable</c> script of the attacker</param>
    /// <param name="target">The <c>Spawnable</c> script of the defender</param>
    /// <returns></returns>
    private int CalculateDamage(Spawnable attacker, Spawnable target)
    {
        // The damage resulting from attacks is always at least 1
        int attack = attacker.attack - target.defense;
        if (attack <= 0)
        {
            attack = 1;
        }
        return attack;
    }

    /// <summary>
    /// This method colors the pawn's border depeneding on the identity of its owner
    /// </summary>
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

    /// <summary>
    /// Simple method that checks if the a set of coordinates are inside the generated board array bounds
    /// </summary>
    /// <param name="board">The current game board</param>
    /// <param name="depthWidth">A Vector2 where the first field is the depth and the second field is the width</param>
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

    /// <summary>
    /// This method scans the neighbouring hexes of the pawn, according to its targeting pattern,
    /// and determines if they are occupied by enemies in order to attack them.
    /// </summary>
    /// <param name="board">The current game board array</param>
    /// <param name="attackerPos">A Vector2 where the first field is the depth and the second field is the width</param>
    /// <returns>A list with the coordinates of the valid targets</returns>
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
                        if (attackerPos.y % 2 == 0)
                            possibleTarget = new Vector2Int(attackerPos.x, attackerPos.y - 1);
                        else
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
                        if (attackerPos.y % 2 == 0)
                            possibleTarget = new Vector2Int(attackerPos.x, attackerPos.y + 1);
                        else
                            possibleTarget = new Vector2Int(attackerPos.x + 1, attackerPos.y +   1);
                        if (areCoordsInsideOfBounds(board, possibleTarget))
                        {
                            targets.Add(possibleTarget);
                        }
                        break;
                    case "BL":
                        if (attackerPos.y % 2 == 0)
                            possibleTarget = new Vector2Int(attackerPos.x - 1, attackerPos.y - 1);
                        else
                            possibleTarget = new Vector2Int(attackerPos.x + 0, attackerPos.y - 1);
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
                        if (attackerPos.y % 2 == 0)
                            possibleTarget = new Vector2Int(attackerPos.x - 1, attackerPos.y + 1);
                        else
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
                        if (attackerPos.y % 2 == 0)
                            possibleTarget = new Vector2Int(attackerPos.x, attackerPos.y - 1);
                        else
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
                        if (attackerPos.y % 2 == 0)
                            possibleTarget = new Vector2Int(attackerPos.x, attackerPos.y + 1);
                        else
                            possibleTarget = new Vector2Int(attackerPos.x + 1, attackerPos.y + 1);
                        if (areCoordsInsideOfBounds(board, possibleTarget))
                        {
                            targets.Add(possibleTarget);
                        }
                        break;
                    case "FR":
                        if (attackerPos.y % 2 == 0)
                            possibleTarget = new Vector2Int(attackerPos.x - 1, attackerPos.y - 1);
                        else
                            possibleTarget = new Vector2Int(attackerPos.x + 0, attackerPos.y - 1);
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
                        if (attackerPos.y % 2 == 0)
                            possibleTarget = new Vector2Int(attackerPos.x - 1, attackerPos.y + 1);
                        else
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

    /// <summary>
    /// Instantieates an attack visual effect
    /// </summary>
    private void AttackFX(int x, int y)
    {
        Vector3 pos = BoardManager.TranslateCoordinates(y, x, transform.position.y);
        GameObject temp = Instantiate(explosionFX, pos, Quaternion.identity);
    }

    /// <summary>
    /// Determine the color of the text representing a pawn's stats, depending on the value of the stats
    /// compared to their original value
    /// </summary>
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

    /// <summary>
    /// Coroutine that handles the animation of a pawn's despawning
    /// </summary>
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

    /// <summary>
    /// Coroutine that handles the animation of a pawn's spawning
    /// </summary>
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
