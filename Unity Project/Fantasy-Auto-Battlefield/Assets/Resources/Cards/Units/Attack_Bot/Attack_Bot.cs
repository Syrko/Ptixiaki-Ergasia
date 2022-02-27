using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack_Bot : MonoBehaviour, IEffectWhenSpawning
{
    public void ExecuteEffect()
    {
        int atkIncrease = FindObjectOfType<HumanPlayer>().Hand.CardsInHandCount;
        gameObject.GetComponent<Unit>().AttackValue += atkIncrease;
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
