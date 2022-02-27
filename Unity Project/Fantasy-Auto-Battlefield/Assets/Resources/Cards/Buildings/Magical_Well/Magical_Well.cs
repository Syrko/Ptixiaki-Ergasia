using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magical_Well : MonoBehaviour, IEffectOnDeath
{
    public void ExecuteDeathEffect()
    {
        FindObjectOfType<HumanPlayer>().GainMana(3);
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
