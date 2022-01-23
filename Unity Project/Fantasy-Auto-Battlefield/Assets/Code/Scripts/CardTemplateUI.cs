using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class CardTemplateUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
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

    private int cardTrayIndex;

    public TextMeshProUGUI CardName { get => cardName.GetComponent<TextMeshProUGUI>(); }
    public TextMeshProUGUI CardCost { get => cardCost.GetComponent<TextMeshProUGUI>(); }
    public Image CardImage { get => cardImage; }
    public TextMeshProUGUI CardText { get => cardText.GetComponent<TextMeshProUGUI>(); }
    public TextMeshProUGUI CardAttack { get => cardAttack.GetComponent<TextMeshProUGUI>(); }
    public TextMeshProUGUI CardDefense { get => cardDefense.GetComponent<TextMeshProUGUI>(); }
    public TextMeshProUGUI CardHitPoints { get => cardHitPoints.GetComponent<TextMeshProUGUI>(); }
    public Image CardHexPattern { get => cardHexPattern; }

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
}
