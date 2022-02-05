using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

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

    Player player;
    Hand hand;

    private int cardTrayIndex;
    private CardType? cardType;

    public CardType? CardType { get => cardType; }
    public int CardCost { get => int.Parse(cardCost.text); }
    public string CardName { get => cardName.text; }

    private void Awake()
    {
        player = FindObjectOfType<Player>();
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

    public void DrawUnit(UnitCardData unit, bool showImmediately)
    {
        cardName.text = unit.CardName;
        cardCost.text = unit.CardCost.ToString();
        cardImage.sprite = unit.CardImage;
        cardText.text = unit.CardText;
        cardAttack.text = unit.Attack.ToString();
        cardDefense.text = unit.Defense.ToString();
        cardHitPoints.text = unit.MaxHitPoints.ToString();
        cardHexPattern.sprite = HexPattern.getHexPatternSprite(unit.AttackPattern);
        cardType = unit.CardType;

        if (showImmediately)
        {
            ShowSelf();
        }
    }

    public void DrawBuilding(BuildingCardData building, bool showImmediately)
    {
        cardName.text = building.CardName;
        cardCost.text = building.CardCost.ToString();
        cardImage.sprite = building.CardImage;
        cardText.text = building.CardText;
        cardAttack.text = building.Attack.ToString();
        cardDefense.text = building.Defense.ToString();
        cardHitPoints.text = building.MaxHitPoints.ToString();
        cardHexPattern.sprite = HexPattern.getHexPatternSprite(building.AttackPattern);
        cardType = building.CardType;

        if (showImmediately)
        {
            ShowSelf();
        }
    }

    public void DrawSpell(SpellCardData spell, bool showImmediately)
    {
        // TODO implement hand ui ShowSpell
        if (showImmediately)
        {
            ShowSelf();
        }
        throw new System.NotImplementedException();
    }

    public void HideSelf()
    {
        gameObject.SetActive(false);
    }

    public void ShowSelf()
    {
        gameObject.SetActive(true);
    }

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
}
