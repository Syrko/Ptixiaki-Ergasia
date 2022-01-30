using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class CardInHand : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
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

    private int cardTrayIndex;
    private CardType? cardType;
    private bool isCardSelected;

    public CardType? CardType { get => cardType; }

    private void Awake()
    {
        cardType = null;
        isCardSelected = false;
        cardTrayIndex = transform.GetSiblingIndex();
        HideSelf();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        cardTrayIndex = transform.GetSiblingIndex();
        transform.SetAsLastSibling();
        transform.localPosition += Vector3.up * displacement;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.SetSiblingIndex(cardTrayIndex);
        transform.localPosition += Vector3.down * displacement;
    }
    
    public void OnPointerDown(PointerEventData eventData)
    {
        /*
        // On right click deselect card
        if(eventData.button == PointerEventData.InputButton.Right)
        {
            isCardSelected = false;
            cardBorder.color = Color.black;
            return;
        }

        // When selecting new card deselect all others...
        foreach(GameObject cardTemplate in mainUI.CardTray)
        {
            CardInHand temp = cardTemplate.GetComponent<CardInHand>();
            if (temp.isCardSelected)
            {
                temp.isCardSelected = false;
                temp.cardBorder.color = Color.black;
            }
        }
        // ...and then select the clicked card
        isCardSelected = true;
        int playerMana = int.Parse(mainUI.Mana.text);
        int cardCost = int.Parse(CardCost.text);
        if (playerMana >= cardCost)
            cardBorder.color = Color.green;
        else
            cardBorder.color = Color.red;
        */
    }

    public void ShowUnit(UnitCardData unit)
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

        ShowSelf();
    }

    public void ShowBuilding(BuildingCardData building)
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

        ShowSelf();
    }

    public void ShowSpell(SpellCardData spell)
    {
        // TODO implement hand ui ShowSpell
        //this.gameObject.SetActive(true);
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
}
