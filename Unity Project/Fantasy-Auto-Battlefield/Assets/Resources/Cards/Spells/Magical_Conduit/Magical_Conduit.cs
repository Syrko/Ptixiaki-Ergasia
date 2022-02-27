using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magical_Conduit : MonoBehaviour, IEffectWithTargetWhenSpawning
{
    public void ExecuteEffect(int depth, int width)
    {
        HumanPlayer player = FindObjectOfType<HumanPlayer>();
        GameObject occupant = FindObjectOfType<GameManager>().Board[depth, width].GetComponent<HexTile>().OccupiedBy;
        if (occupant != null)
        {
            occupant.GetComponent<Spawnable>().TakeDamage(1);
            player.GainMana(player.Hand.CardsInHandCount);
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