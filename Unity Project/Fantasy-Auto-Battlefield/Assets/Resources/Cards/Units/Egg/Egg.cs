using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Egg : MonoBehaviour, IEffectOnDeath, IEffectWithTargetWhenSpawning
{
    private int depth;
    private int width;

    public void ExecuteDeathEffect()
    {
        bool forPlayer = gameObject.GetComponent<Unit>().Owner is HumanPlayer;
        UnitFactory.CreateUnitPawn("Strong_Birb", width, depth, forPlayer);
    }

    public void ExecuteEffect(int depth, int width)
    {
        this.depth = depth;
        this.width = width;
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
