using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    protected int frontline;
    protected bool hasInitiative;

    public bool HasInitiative { get => hasInitiative; set => hasInitiative = value; }
    public int Frontline { get => frontline; }
}
