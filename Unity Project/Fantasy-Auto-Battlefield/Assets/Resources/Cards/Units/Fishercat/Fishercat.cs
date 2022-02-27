using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fishercat : MonoBehaviour, IEffectOnDeath
{
    public void ExecuteDeathEffect()
    {
        FindObjectOfType<HumanPlayer>().DrawCardFromDeck();
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
