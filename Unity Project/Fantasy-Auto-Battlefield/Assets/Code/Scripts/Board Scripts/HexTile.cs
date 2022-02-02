using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexTile : MonoBehaviour
{
    [SerializeField, Tooltip("The data of the hex according to its category (e.g forest)")]
    TileData tileData;

    GameObject occupiedBy = null;
    
    TileHeight tileHeight;
    int posX;
    int posY;

    public GameObject OccupiedBy { get => occupiedBy; set => occupiedBy = value; }

	private void OnMouseDown()
	{
        SubjectUI.Notify(tileData, new EventUI(EventUICodes.TILE_INFO_CHANGED));
	}
}
