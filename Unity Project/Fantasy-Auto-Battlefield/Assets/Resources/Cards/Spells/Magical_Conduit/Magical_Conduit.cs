using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magical_Conduit : MonoBehaviour, IEffectWhenSpawning
{
    public void ExecuteEffect()
    {
        HumanPlayer player = FindObjectOfType<HumanPlayer>();
        player.GainMana(player.Hand.CardsInHandCount);

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
