using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardTemplateUI : MonoBehaviour
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
}
