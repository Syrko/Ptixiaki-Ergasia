using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disintegrate : MonoBehaviour, IEffectWithTargetWhenSpawning
{
    public void ExecuteEffect(int depth, int width)
    {
        GameObject occupant = FindObjectOfType<GameManager>().Board[depth, width].GetComponent<HexTile>().OccupiedBy;
        if (occupant != null)
        {
            occupant.GetComponent<Spawnable>().Die();
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
