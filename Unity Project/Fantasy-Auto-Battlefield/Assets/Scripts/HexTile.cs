using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexTile : MonoBehaviour
{
    [SerializeField, Tooltip("The data of the hex according to its category (e.g forest)")]
    TileData tileData;
    
    TileHeight tileHeight;
    int posX;
    int posY;

    /// <summary>
    /// Should be called immediately after instantiation
    /// </summary>
	public void Initialize()
	{
		
	}
}
