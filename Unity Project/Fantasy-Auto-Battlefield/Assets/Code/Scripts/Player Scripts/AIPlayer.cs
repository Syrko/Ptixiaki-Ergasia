using System.Collections;
using System;
using UnityEngine;

public class AIPlayer : Player
{
    [SerializeField]
    GameManager gameManager;
    [SerializeField]
    int maxFrontline = 3;

    private void Awake()
    {
        maxHP = gameManager.MaxHP;
        currentHP = gameManager.MaxHP;

        maxFrontline = gameManager.BoardDepth - maxFrontline; // Reversing the frontline of the AI
        frontline = 0;
        hasInitiative = false;
    }

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

    private string DecideOnSpawnableCardToPlay(SpawnableTiers tier)
    {
        string[] LowTier = { CardCatalogue.Soldier.CardName, CardCatalogue.Little_Imp.CardName, CardCatalogue.Crabby.CardName, CardCatalogue.Egg_Thief.CardName };
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

    public void PlayAICards()
    {
        if (FlipCoin())
        {
            // Half of the turn the AI will not play anything - EZ mode
            return;
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

    private bool FlipCoin()
    {
        return UnityEngine.Random.Range(0f, 1f) < 0.5f;
    }

    private enum SpawnableTiers
    {
        Low, Medium, High
    }
}
