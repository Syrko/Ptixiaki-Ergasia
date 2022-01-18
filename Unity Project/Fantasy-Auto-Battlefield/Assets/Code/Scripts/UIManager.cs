using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [Header("Card Info")]
    [SerializeField]
    TextMeshProUGUI CardName;
    [SerializeField]
    TextMeshProUGUI CardCost;
    [SerializeField]
    Image CardImage;
    [SerializeField]
    TextMeshProUGUI CardText;
    [SerializeField]
    TextMeshProUGUI CardAttack;
    [SerializeField]
    TextMeshProUGUI CardDefense;
    [SerializeField]
    TextMeshProUGUI CardHitPoints;
    [SerializeField]
    Image HexPattern;
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
    Button EndPhase;
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

    [Space(10)]

    [SerializeField]
    GameObject[] CardTray;

    public void UpdateTileInfo(TileData tileData)
	{
        TileImage.sprite = tileData.TileImage;
        TileName.text = tileData.TileType.ToString();
        TileText.text = tileData.TileDescription;
	}

    private void UpdateCardInfo(Unit data)
    {
        CardName.text = data.CardName;
        CardCost.text = data.CardCost.ToString();
        CardImage.sprite = data.CardImage;
        CardText.text = data.CardText;
        CardAttack.text = data.AttackValue.ToString();
        CardDefense.text = data.Defense.ToString();
        CardHitPoints.text = data.CurrentHP.ToString();
        HexPattern.sprite = data.AttackPattern;
    }
    
    private void UpdateCardInfo(BuildingCardData data)
    {

    }
    
    private void UpdateCardInfo(SpellCardData data)
    {

    }
}
