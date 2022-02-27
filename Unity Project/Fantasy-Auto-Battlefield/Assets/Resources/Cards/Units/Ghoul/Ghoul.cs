using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghoul : MonoBehaviour, IEffectWhenSpawning
{
    public void ExecuteEffect()
    {
        gameObject.GetComponent<Unit>().TakeDamage(4);
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
