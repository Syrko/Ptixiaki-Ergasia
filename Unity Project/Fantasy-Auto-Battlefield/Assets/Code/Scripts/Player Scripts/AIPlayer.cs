using System.Collections;
using System;
using UnityEngine;

/// <summary>
/// <c>AIPlayer</c> is a monobeahaviour that inherits from the <c>Player</c> class.
/// It represents the oppontent of the user.
/// </summary>
public class AIPlayer : Player
{
    [SerializeField]
    GameManager gameManager;
    [SerializeField]
    int maxFrontline = 3;

    private int turnCounter = 0;
    public int TurnCounter { get => turnCounter; set => turnCounter = value; }

    private void Awake()
    {
        maxHP = gameManager.MaxHP;
        currentHP = gameManager.MaxHP;

        maxFrontline = gameManager.BoardDepth - maxFrontline; // Reversing the frontline of the AI
        frontline = 0;
        hasInitiative = false;
    }

    /// <summary>
    /// Determines the front line of the AI player.
    /// It is not included in the shared bahaviour of the <c>Player</c> class, 
    /// to account for the different perspective of the board.
    /// </summary>
    private void DetermineFrontLine(GameObject[,] board)
    {
        int tempFrontLine = gameManager.BoardDepth - 1;

        // Checking every row for occupant nad check its owner
        for (int depth = gameManager.BoardDepth - 1; depth >= maxFrontline; depth--)
        {
            for (int width = 0; width < gameManager.BoardWidth; width++)
            {
                GameObject occupant = board[depth, width].GetComponent<HexTile>().OccupiedBy;
                if (occupant != null)
                {
                    if (occupant.GetComponent<Spawnable>().Owner is AIPlayer)
                    {
                        if (depth < tempFrontLine && depth >= maxFrontline)
                        {
                            tempFrontLine = depth;
                        }
                    }
                }
            }
        }
        frontline = tempFrontLine;
    }

    /// <summary>
    /// This method chooses randomly a card to play from a pool of cards.
    /// </summary>
    /// <param name="tier">The pool from which the card to be played wil be chosen.</param>
    /// <returns></returns>
    private string DecideOnSpawnableCardToPlay(SpawnableTiers tier)
    {
        string[] LowTier = { CardCatalogue.Soldier.CardName, CardCatalogue.Soldier.CardName, CardCatalogue.Little_Imp.CardName, CardCatalogue.Crabby.CardName, CardCatalogue.Egg_Thief.CardName };
        string[] MidTier = { CardCatalogue.Mad_Slug.CardName, CardCatalogue.Yeti.CardName, CardCatalogue.Egg.CardName, CardCatalogue.Ghoul.CardName, CardCatalogue.Mad_Slug.CardName, CardCatalogue.Yeti.CardName, CardCatalogue.Egg.CardName, CardCatalogue.Ghoul.CardName, CardCatalogue.Healing_Sheep.CardName, CardCatalogue.Gate.CardName };
        string[] HighTier = { CardCatalogue.The_Seventh_Demon.CardName, CardCatalogue.The_Seventh_Demon.CardName, CardCatalogue.The_Seventh_Demon.CardName, CardCatalogue.The_Great_Golem.CardName, CardCatalogue.The_Seventh_Demon.CardName, CardCatalogue.The_Seventh_Demon.CardName, CardCatalogue.The_Seventh_Demon.CardName, CardCatalogue.The_Great_Golem.CardName, CardCatalogue.Guard_Tower.CardName, CardCatalogue.Cursed_Ruins.CardName };

        int choice;
        switch (tier)
        {
            case SpawnableTiers.Low:
                choice = UnityEngine.Random.Range(0, LowTier.Length);
                return LowTier[choice];
            case SpawnableTiers.Medium:
                choice = UnityEngine.Random.Range(0, LowTier.Length);
                return MidTier[choice];
            case SpawnableTiers.High:
                choice = UnityEngine.Random.Range(0, LowTier.Length);
                return HighTier[choice];
            default:
                return LowTier[0];
        }
    }

    /// <summary>
    /// This method decides where on the board (inside the limits of the frontline) will the AI play its card.
    /// </summary>
    /// <returns>The coordinates of the target hex on the board</returns>
    private Vector2Int DecidePosition()
    {
        DetermineFrontLine(gameManager.Board);
        int depth;
        int width;

        int triesToFindPos = 10;
        do
        {
            triesToFindPos--;
            if (triesToFindPos == 0)
            {
                return new Vector2Int(-1 , -1);
            }
            depth = UnityEngine.Random.Range(frontline, gameManager.BoardDepth);
            width = UnityEngine.Random.Range(0, gameManager.BoardWidth);
        } while (gameManager.Board[depth, width].GetComponent<HexTile>().OccupiedBy != null);

        return new Vector2Int(depth, width);
    }

    /// <summary>
    /// The bahaviour of the AI. The logic with which it determines what cards to play.
    /// In the Single iteration, the AI progresses through waves from easier to harder depending on the amount of turns played.
    /// When the hardest wave is reached, tthe AI keeps playing cards from that category.
    /// !-- NOT CURRENTLY IN USE --!
    /// </summary>
    public void PlayAICardsSingle()
    {
        if (gameManager.GameTurnsPlayed < 10)
        {
            if (FlipCoin())
            {
                // Half of the turns the AI will not play anything - EZ mode
                return;
            }
        }
        else
        {
            if (FlipCoin() && FlipCoin())
            {
                // 25% of the turns the AI will not play anything - EZ mode
                return;
            }
        }

        if (gameManager.GameTurnsPlayed < 4)
        {
            PlaySpawnable(SpawnableTiers.Low);            
        }
        else if (gameManager.GameTurnsPlayed < 6)
        {
            if (FlipCoin())
            {
                PlaySpawnable(SpawnableTiers.Low);
                PlaySpawnable(SpawnableTiers.Low);
            }
            else
            {
                PlaySpawnable(SpawnableTiers.Medium);
            }
        }
        else if (gameManager.GameTurnsPlayed < 9)
        {
            if (FlipCoin())
            {
                PlaySpawnable(SpawnableTiers.Low);
                PlaySpawnable(SpawnableTiers.Low);
            }
            else
            {
                PlaySpawnable(SpawnableTiers.Medium);
                PlaySpawnable(SpawnableTiers.Medium);
            }
        }
        else if (gameManager.GameTurnsPlayed < 12)
        {
            if (FlipCoin())
            {
                PlaySpawnable(SpawnableTiers.Low);
                PlaySpawnable(SpawnableTiers.Low);
                PlaySpawnable(SpawnableTiers.Medium);
            }
            else
            {
                if (FlipCoin())
                {
                    PlaySpawnable(SpawnableTiers.High);
                }
                else
                {
                    PlaySpawnable(SpawnableTiers.Medium);
                    PlaySpawnable(SpawnableTiers.Medium);
                }
            }
        }
        else
        {
            if (FlipCoin())
            {
                PlaySpawnable(SpawnableTiers.High);
                PlaySpawnable(SpawnableTiers.Low);
            }
            else
            {
                if (FlipCoin())
                {
                    PlaySpawnable(SpawnableTiers.Medium);
                    PlaySpawnable(SpawnableTiers.Medium);
                    PlaySpawnable(SpawnableTiers.Low);
                }
                else
                {
                    PlaySpawnable(SpawnableTiers.Low);
                    PlaySpawnable(SpawnableTiers.Low);
                    PlaySpawnable(SpawnableTiers.Low);
                    PlaySpawnable(SpawnableTiers.Medium);
                }
            }
        }
    }

    /// <summary>
    /// The bahaviour of the AI. The logic with which it determines what cards to play.
    /// In the Cycle iteration, the AI progresses through waves from easier to harder depending on the amount of turns played.
    /// When a number of turns is reached, the turn counter resets.
    /// </summary>
    public void PlayAICardsCycle()
    {
        if(turnCounter >= 12)
        {
            turnCounter = 1;
        }
        if (FlipCoin() && FlipCoin())
        {
            // 25% of the turns the AI will not play anything
            return;
        }

        if (turnCounter < 3)
        {
            PlaySpawnable(SpawnableTiers.Low);
        }
        else if (turnCounter < 5)
        {
            if (FlipCoin())
            {
                PlaySpawnable(SpawnableTiers.Low);
                PlaySpawnable(SpawnableTiers.Low);
            }
            else
            {
                PlaySpawnable(SpawnableTiers.Medium);
            }
        }
        else if (turnCounter < 8)
        {
            if (FlipCoin())
            {
                PlaySpawnable(SpawnableTiers.Low);
                PlaySpawnable(SpawnableTiers.Low);
            }
            else
            {
                PlaySpawnable(SpawnableTiers.Medium);
                PlaySpawnable(SpawnableTiers.Medium);
            }
        }
        else if (turnCounter < 10)
        {
            if (FlipCoin())
            {
                PlaySpawnable(SpawnableTiers.Low);
                PlaySpawnable(SpawnableTiers.Low);
                PlaySpawnable(SpawnableTiers.Medium);
            }
            else
            {
                if (FlipCoin())
                {
                    PlaySpawnable(SpawnableTiers.High);
                }
                else
                {
                    PlaySpawnable(SpawnableTiers.Medium);
                    PlaySpawnable(SpawnableTiers.Medium);
                }
            }
        }
        else if (turnCounter < 12)
        {
            if (FlipCoin())
            {
                PlaySpawnable(SpawnableTiers.High);
                PlaySpawnable(SpawnableTiers.Low);
            }
            else
            {
                if (FlipCoin())
                {
                    PlaySpawnable(SpawnableTiers.Medium);
                    PlaySpawnable(SpawnableTiers.Medium);
                    PlaySpawnable(SpawnableTiers.Low);
                }
                else
                {
                    PlaySpawnable(SpawnableTiers.Low);
                    PlaySpawnable(SpawnableTiers.Low);
                    PlaySpawnable(SpawnableTiers.Low);
                    PlaySpawnable(SpawnableTiers.Medium);
                }
            }
        }
    }

    /// <summary>
    /// A method that encompasses the logic of determining which card and where the AI will play.
    /// </summary>
    /// <param name="tier"></param>
    private void PlaySpawnable(SpawnableTiers tier)
    {
        string cardToPlay = DecideOnSpawnableCardToPlay(tier);
        Vector2Int depthWidth = DecidePosition();

        if (depthWidth.x == -1) // In case DecidePosition didn't find an empty hex in 10 tries
        {
            return;
        }
        else
        {
            gameManager.SpawnPawn(cardToPlay, (int)depthWidth.y, (int)depthWidth.x, false);
            GameLog.Log(this.gameObject, new LogEvent(LogEventCode.CardPlayed, cardToPlay));
        }
    }

    /// <summary>
    /// Helper method that returns a boolean value with a 1/2 chance.
    /// Used to assist in the AI logic
    /// </summary>
    private bool FlipCoin()
    {
        return UnityEngine.Random.Range(0f, 1f) < 0.5f;
    }

    /// <summary>
    /// Small enum used for the tiers of card pools available to the AI
    /// </summary>
    private enum SpawnableTiers
    {
        Low, Medium, High
    }
}
