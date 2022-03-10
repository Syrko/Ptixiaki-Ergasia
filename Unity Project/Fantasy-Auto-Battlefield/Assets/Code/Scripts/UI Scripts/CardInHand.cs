using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// Each <c>CardInHand</c> contains the behaviour for each of the five cards, that the human player may have drawn.
/// </summary>
public class CardInHand : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    public static bool cardIsBeingPlayed;

    private const float displacement = 120f;

    [Header("Card Info")]
    [SerializeField]
    TextMeshProUGUI cardName;
    [SerializeField]
    TextMeshProUGUI cardCost;
    [SerializeField]
    Image cardImage;
    [SerializeField]
    TextMeshProUGUI cardText;
    [SerializeField]
    TextMeshProUGUI cardAttack;
    [SerializeField]
    TextMeshProUGUI cardDefense;
    [SerializeField]
    TextMeshProUGUI cardHitPoints;
    [SerializeField]
    Image cardHexPattern;
    [SerializeField]
    Image cardBorder;
    [SerializeField]
    TextMeshProUGUI cardTypeText;
    [SerializeField]
    Image attackIcon;
    [SerializeField]
    Image defenseIcon;
    [SerializeField]
    Image hitpointsIcon;

    HumanPlayer player;
    Hand hand;

    private int cardTrayIndex;
    private CardType? cardType;

    public CardType? CardType { get => cardType; }
    public int CardCost { get => int.Parse(cardCost.text); }
    public string CardName { get => cardName.text.Replace(' ', '_'); }

    private void Awake()
    {
        player = FindObjectOfType<HumanPlayer>();
        hand = FindObjectOfType<Hand>();
        cardType = null;
        cardTrayIndex = transform.GetSiblingIndex();
        HideSelf();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (cardIsBeingPlayed)
            return;
        cardTrayIndex = transform.GetSiblingIndex();
        transform.SetAsLastSibling();
        transform.localPosition += Vector3.up * displacement;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (cardIsBeingPlayed)
            return;
        transform.SetSiblingIndex(cardTrayIndex);
        transform.localPosition += Vector3.down * displacement;
    }
    
    public void OnPointerDown(PointerEventData eventData)
    {
        if (cardIsBeingPlayed)
            return;
        hand.onCardSelection(cardTrayIndex);
    }

    /// <summary>
    /// Displays a unit card
    /// </summary>
    public void DrawUnit(UnitCardData unit, bool showImmediately)
    {
        DisableSpellMode();

        cardName.text = unit.CardName.Replace('_', ' ');
        cardCost.text = unit.CardCost.ToString();
        cardImage.sprite = unit.CardImage;
        cardText.text = unit.CardText;
        cardAttack.text = unit.Attack.ToString();
        cardDefense.text = unit.Defense.ToString();
        cardHitPoints.text = unit.MaxHitPoints.ToString();
        cardHexPattern.sprite = HexPattern.getHexPatternSprite(unit.AttackPattern);
        cardType = unit.CardType;
        cardTypeText.text = cardType.ToString();

        if (showImmediately)
        {
            ShowSelf();
        }
    }

    /// <summary>
    /// Displays a building card
    /// </summary>
    public void DrawBuilding(BuildingCardData building, bool showImmediately)
    {
        DisableSpellMode();

        cardName.text = building.CardName.Replace('_', ' ');
        cardCost.text = building.CardCost.ToString();
        cardImage.sprite = building.CardImage;
        cardText.text = building.CardText;
        cardAttack.text = building.Attack.ToString();
        cardDefense.text = building.Defense.ToString();
        cardHitPoints.text = building.MaxHitPoints.ToString();
        cardHexPattern.sprite = HexPattern.getHexPatternSprite(building.AttackPattern);
        cardType = building.CardType;
        cardTypeText.text = cardType.ToString();

        if (showImmediately)
        {
            ShowSelf();
        }
    }

    /// <summary>
    /// Displays a spell card
    /// </summary>
    public void DrawSpell(SpellCardData spell, bool showImmediately)
    {
        EnableSpellMode();

        cardName.text = spell.CardName.Replace('_', ' ');
        cardCost.text = spell.CardCost.ToString();
        cardImage.sprite = spell.CardImage;
        cardText.text = spell.CardText;
        cardType = spell.CardType;
        cardTypeText.text = cardType.ToString();

        if (showImmediately)
        {
            ShowSelf();
        }
    }

    public void HideSelf()
    {
        gameObject.SetActive(false);
    }

    public void ShowSelf()
    {
        gameObject.SetActive(true);
    }

    /// <summary>
    /// Updates the border of a card's border. The color changes appropriately if the card is selected or not,
    /// and if the player has enough mana to play it.
    /// </summary>
    public void UpdateBorder(bool selected)
    {
        if (selected)
        {
            if(player.CurrentMana >= int.Parse(cardCost.text))
            {
                cardBorder.color = Color.green;
            }
            else
            {
                cardBorder.color = Color.red;
            }
        }
        else
        {
            cardBorder.color = Color.black;
        }
    }

    public void EmptyCardType()
    {
        cardType = null;
    }

    /// <summary>
    /// This method changes the components of the card template in order to accomodate the graphics of a Spell card
    /// </summary>
    private void EnableSpellMode()
    {
        attackIcon.gameObject.SetActive(false);
        defenseIcon.gameObject.SetActive(false);
        hitpointsIcon.gameObject.SetActive(false);
        cardAttack.gameObject.SetActive(false);
        cardDefense.gameObject.SetActive(false);
        cardHitPoints.gameObject.SetActive(false);
        cardHexPattern.gameObject.SetActive(false);
    }

    /// <summary>
    /// This method changes the components of the card template in order to accomodate the graphics of a Unit or Building card
    /// </summary>
    private void DisableSpellMode()
    {
        attackIcon.gameObject.SetActive(true);
        defenseIcon.gameObject.SetActive(true);
        hitpointsIcon.gameObject.SetActive(true);
        cardAttack.gameObject.SetActive(true);
        cardDefense.gameObject.SetActive(true);
        cardHitPoints.gameObject.SetActive(true);
        cardHexPattern.gameObject.SetActive(true);
    }
}
