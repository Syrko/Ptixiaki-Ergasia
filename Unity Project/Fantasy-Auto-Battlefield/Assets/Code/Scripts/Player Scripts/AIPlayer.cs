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

    private string DecideOnSpawnableCardToPlay()
    {
        // TODO implement DecideOnCardToPlay() for the AI
        return CardCatalog.Soldier;
    }

    private Vector2 DecidePosition()
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
                return new Vector2(-1 , -1);
            }
            depth = UnityEngine.Random.Range(frontline, gameManager.BoardDepth);
            width = UnityEngine.Random.Range(0, gameManager.BoardWidth);
        } while (gameManager.Board[depth, width].GetComponent<HexTile>().OccupiedBy != null);

        return new Vector2(depth, width);
    }

    public void PlaySpawnableCard()
    {
        string cardToPlay = DecideOnSpawnableCardToPlay();
        Vector2 depthWidth = DecidePosition();
        if(depthWidth.x == -1) // In case DecidePosition didn't find an empty hex in 10 tries
        {
            return;
        }
        else
        {
            gameManager.SpawnPawn(cardToPlay, (int)depthWidth.y, (int)depthWidth.x, false);
        }
    }
}
