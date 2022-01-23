using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BoardUI : MonoBehaviour
{
    [SerializeField]
    GameObject opponentHP;
    [SerializeField]
    GameObject playerHP;
    [SerializeField]
    GameObject playerDeckCounter;
    [SerializeField]
    GameObject initiativeToken;

    public TextMeshPro OpponentHP { get => opponentHP.GetComponent<TextMeshPro>(); }
    public TextMeshPro PlayerHP { get => playerHP.GetComponent<TextMeshPro>(); }
    public TextMeshPro PlayerDeckCounter { get => playerDeckCounter.GetComponent<TextMeshPro>(); }
    public GameObject InitiativeToken { get => initiativeToken; }
}
