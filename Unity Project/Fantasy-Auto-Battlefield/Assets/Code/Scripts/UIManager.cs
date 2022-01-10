using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [Header("Card Info")]
    [SerializeField]
    Image CardImage;
    [SerializeField]
    TextMeshProUGUI CardText;
    [Space(10)]
    
    [Header("Tile Info")]
    [SerializeField]
    Image TileImage;
    [SerializeField]
    TextMeshProUGUI TileName;
    [SerializeField]
    TextMeshProUGUI TileText;
    [Space(10)]

    [Header("Buttons")]
    [SerializeField]
    Button PlayCard;
    [SerializeField]
    Button ToggleCards;
    [SerializeField]
    Text ToggleCardsText;
    [SerializeField]
    Button Menu;

    [Space(10)]

    [Header("Status Bar")]
    [SerializeField]
    TextMeshProUGUI Mana;
    [SerializeField]
    TextMeshProUGUI Phase;
    [SerializeField]
    TextMeshProUGUI HitPoints;

    public void UpdateTileInfo(TileData tileData)
	{
        TileImage.sprite = tileData.TileImage;
        TileName.text = tileData.TileType.ToString();
        TileText.text = tileData.TileDescription;
	}
}
