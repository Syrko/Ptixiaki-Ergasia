using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainUI : MonoBehaviour, IObserverUI
{
    [Header("Card Info")]
    [SerializeField]
    Image cardCover;
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
    Button playCard;
    [SerializeField]
    Button toggleCards;
    [SerializeField]
    Text toggleCardsText;
    [SerializeField]
    Button endPhase;
    [SerializeField]
    Button Menu;

    [Space(10)]

    [Header("Status Bar")]
    [SerializeField]
    TextMeshProUGUI mana;
    [SerializeField]
    TextMeshProUGUI phase;
    [SerializeField]
    TextMeshProUGUI hitPoints;

    public TextMeshProUGUI Mana { get => mana; }
    public TextMeshProUGUI Phase { get => phase; }
    public TextMeshProUGUI HitPoints { get => hitPoints; }
    public Button ToggleCards { get => toggleCards; }
    public Text ToggleCardsText { get => toggleCardsText; }
    public Image CardCover { get => cardCover; }
    public Button EndPhase { get => endPhase; }
    public Button PlayCard { get => playCard; }

    private void Awake()
    {
        SubjectUI.AddObserver(this);
    }
 
    public void onNotify(GameObject sender, EventUI eventData)
    {
        switch (eventData.Code)
        {
            case EventUICodes.CARD_INFO_CHANGED:
                UpdateCardInfo(sender);
                break;
            case EventUICodes.PLAYER_MANA_CHANGED:
                Mana.text = eventData.Value;
                break;
            case EventUICodes.PLAYER_HP_CHANGED:
                HitPoints.text = eventData.Value;
                break;
            case EventUICodes.PHASE_CHANGED:
                break;

        }
    }

    public void onNotify(ScriptableObject objectData, EventUI eventData)
    {
        switch (eventData.Code)
        {
            case EventUICodes.TILE_INFO_CHANGED:
                if (objectData is TileData)
                {
                    UpdateTileInfo(objectData as TileData);
                }
                break;
        }
    }

    private void UpdateTileInfo(TileData tileData)
    {
        TileImage.sprite = tileData.TileImage;
        TileName.text = tileData.TileType.ToString();
        TileText.text = tileData.TileDescription;
    }

    private void UpdateCardInfo(GameObject sender)
    {
        Component component;
        if (sender.TryGetComponent(typeof(Unit), out component))
        {
            UpdateCardInfo(component as Unit);
        }
        else if (sender.TryGetComponent(typeof(Building), out component))
        {
            UpdateCardInfo(component as Building);
        }
        else if (sender.TryGetComponent(typeof(Spell), out component))
        {
            UpdateCardInfo(component as Spell);
        }
    }

    private void UpdateCardInfo(Unit data)
    {
        cardCover.gameObject.SetActive(false);

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

    private void UpdateCardInfo(Building data)
    {
        cardCover.gameObject.SetActive(false);

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

    private void UpdateCardInfo(Spell data)
    {
        cardCover.gameObject.SetActive(false);
        // TODO implement UpdateCardInfo for spells
        throw new NotImplementedException();
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
}
