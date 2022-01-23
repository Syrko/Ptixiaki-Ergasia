using System;
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
    Image CardHexPattern;
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

    private void Awake()
    {
        foreach (var card in CardTray)
        {
            card.gameObject.SetActive(false);
        }
    }

    public void UpdateTileInfo(TileData tileData)
	{
        TileImage.sprite = tileData.TileImage;
        TileName.text = tileData.TileType.ToString();
        TileText.text = tileData.TileDescription;
	}

    private Color DetermineValueColor(int originalValue, int currentCalue)
    {
        if (originalValue > currentCalue)
        {
            return Color.red;
        } 
        else if (originalValue < currentCalue) 
        {
            return Color.green;
        }
        else
        {
            return Color.white;
        }
    }

    public void UpdateCardInfo(Unit data)
    {
        CardName.text = data.CardName;
        
        CardCost.text = data.CardCost.ToString();
        CardCost.color = DetermineValueColor(data.OriginalCardCost, data.CardCost);

        CardImage.sprite = data.CardImage;

        CardText.text = data.CardText;

        CardAttack.text = data.AttackValue.ToString();
        CardAttack.color = DetermineValueColor(data.OriginalAttackValue, data.AttackValue);

        CardDefense.text = data.Defense.ToString();
        CardDefense.color = DetermineValueColor(data.OriginalDefense, data.Defense);

        CardHitPoints.text = data.CurrentHP.ToString();
        CardHitPoints.color = DetermineValueColor(data.MaxHitPoints, data.CurrentHP);

        CardHexPattern.sprite = HexPattern.getHexPatternSprite(data.AttackPattern); 
    }
    
    public void UpdateCardInfo(Building data)
    {
        CardName.text = data.CardName;

        CardCost.text = data.CardCost.ToString();
        CardCost.color = DetermineValueColor(data.OriginalCardCost, data.CardCost);

        CardImage.sprite = data.CardImage;

        CardText.text = data.CardText;

        CardAttack.text = data.AttackValue.ToString();
        CardAttack.color = DetermineValueColor(data.OriginalAttackValue, data.AttackValue);

        CardDefense.text = data.Defense.ToString();
        CardDefense.color = DetermineValueColor(data.OriginalDefense, data.Defense);

        CardHitPoints.text = data.CurrentHP.ToString();
        CardHitPoints.color = DetermineValueColor(data.MaxHitPoints, data.CurrentHP);

        CardHexPattern.sprite = HexPattern.getHexPatternSprite(data.AttackPattern);
    }
    
    private void UpdateCardInfo(SpellCardData data)
    {
        // TODO implement UpdateCardInfo for spells
        throw new NotImplementedException();
    }

    public void ShowCardInHand(int index, string card)
    {
        switch (CardCatalog.GetType(card))
        {
            case CardType.Unit:
                ShowUnitInHand(index, Resources.Load<UnitCardData>("Cards/Units/" + card + "/" + card));
                break;
            case CardType.Building:
                ShowBuildingInHand(index, Resources.Load<BuildingCardData>("Cards/Buildings/" + card + "/" + card));
                break;
            case CardType.Spell:
                ShowSpellInHand(index, Resources.Load<SpellCardData>("Cards/Spells/" + card + "/" + card));
                break;
            case null:
                // TODO implement handling of null value
                throw new NotImplementedException();
        }

    }

    private void ShowUnitInHand(int index, UnitCardData data)
    {

    }
    
    private void ShowBuildingInHand(int index, BuildingCardData data)
    {

    }

    private void ShowSpellInHand(int index, SpellCardData data)
    {
        // TODO implement
        throw new NotImplementedException();
    }
}
