using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class CardTemplateUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    private const float displacement = 120f;

    [Header("Card Info")]
    [SerializeField]
    GameObject cardName;
    [SerializeField]
    GameObject cardCost;
    [SerializeField]
    Image cardImage;
    [SerializeField]
    GameObject cardText;
    [SerializeField]
    GameObject cardAttack;
    [SerializeField]
    GameObject cardDefense;
    [SerializeField]
    GameObject cardHitPoints;
    [SerializeField]
    Image cardHexPattern;
    [SerializeField]
    Image cardBorder;

    private int cardTrayIndex;
    private bool isCardFilled = false;
    private bool isCardSelected = false;

    private MainUI mainUI;

    public TextMeshProUGUI CardName { get => cardName.GetComponent<TextMeshProUGUI>(); }
    public TextMeshProUGUI CardCost { get => cardCost.GetComponent<TextMeshProUGUI>(); }
    public Image CardImage { get => cardImage; }
    public TextMeshProUGUI CardText { get => cardText.GetComponent<TextMeshProUGUI>(); }
    public TextMeshProUGUI CardAttack { get => cardAttack.GetComponent<TextMeshProUGUI>(); }
    public TextMeshProUGUI CardDefense { get => cardDefense.GetComponent<TextMeshProUGUI>(); }
    public TextMeshProUGUI CardHitPoints { get => cardHitPoints.GetComponent<TextMeshProUGUI>(); }
    public Image CardHexPattern { get => cardHexPattern; }
    public bool IsCardFilled { get => isCardFilled; set => isCardFilled = value; }

    private void Awake()
    {
        mainUI = FindObjectOfType<MainUI>();
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
            CardTemplateUI temp = cardTemplate.GetComponent<CardTemplateUI>();
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
    }
}
