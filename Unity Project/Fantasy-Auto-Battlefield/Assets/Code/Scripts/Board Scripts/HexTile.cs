using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexTile : MonoBehaviour
{
    [SerializeField, Tooltip("The data of the hex according to its category (e.g forest)")]
    TileData tileData;

    GameObject occupiedBy = null;
    GameObject highlightHex;
    
    TileHeight tileHeight;
    int posX;
    int posY;

    public GameObject OccupiedBy { get => occupiedBy; set => occupiedBy = value; }

    private void Awake()
    {
        highlightHex = transform.Find("Highlight").gameObject;
        Highlight(false);
    }

    private void OnMouseDown()
	{
        SubjectUI.Notify(tileData, new UIEvent(EventUICodes.TILE_INFO_CHANGED));
	}

    public void Highlight(bool highlightON)
    {
        highlightHex.SetActive(highlightON);
    }
}
