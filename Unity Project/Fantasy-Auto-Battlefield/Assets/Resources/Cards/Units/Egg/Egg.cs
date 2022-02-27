using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Egg : MonoBehaviour, IEffectOnDeath
{
    private int depth;
    private int width;

    public void ExecuteDeathEffect()
    {
        GameManager gm = FindObjectOfType<GameManager>();
        foreach (GameObject tile in gm.Board)
        {
            HexTile hex = tile.GetComponent<HexTile>();
            if (hex.OccupiedBy == this.gameObject)
            {
                depth = hex.PosY;
                width = hex.PosX;

                bool forPlayer = gameObject.GetComponent<Unit>().Owner is HumanPlayer;
                GameObject pawn = UnitFactory.CreateUnitPawn("Strong_Birb", width, depth, forPlayer);
                gm.Board[depth, width].GetComponent<HexTile>().OccupiedBy = pawn;
                gm.ExecuteTerrainEffects(depth, width, pawn, true);

                return;
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDestroy()
    {
        ExecuteDeathEffect();
    }
}
