using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Summoner : MonoBehaviour, IEffectWhenSpawning
{
    public void ExecuteEffect()
    {
        FindObjectOfType<HumanPlayer>().DrawCardFromDeck();
    }

    // Start is called before the first frame update
    void Start()
    {
        ExecuteEffect();        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
