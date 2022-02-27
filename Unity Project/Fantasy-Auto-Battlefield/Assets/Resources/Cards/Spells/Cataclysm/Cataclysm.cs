using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cataclysm : MonoBehaviour, IEffectWithTargetWhenSpawning
{
    public void ExecuteEffect(int depth, int width)
    {
        GameManager gm = FindObjectOfType<GameManager>();
        GameObject[,] board = gm.Board;
        for(int d = 0; d < gm.BoardDepth; d++)
        {
            for(int w = 0; w < gm.BoardWidth; w++)
            {
                if(board[d, w].GetComponent<HexTile>().OccupiedBy != null)
                {
                    if (d == depth && w == width)
                    {
                        continue;
                    }
                    else
                    { 
                        board[d, w].GetComponent<HexTile>().OccupiedBy.GetComponent<Spawnable>().Die();
                    }
                }
            }
        }

        Destroy(this.gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
