using System;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    List<string> cards;

    MainUI ui;
    CardTemplateUI[] cardsInHandUI;

    bool isHandShown;
    public int CardCount { get { return cards.Count; } }

    private void Awake()
    {
        cards = new List<string>();
        ui = FindObjectOfType<MainUI>();
        cardsInHandUI = FindObjectsOfType<CardTemplateUI>(true);
        isHandShown = true;

        ui.ToggleCards.onClick.AddListener(ToggleHand);
    }

    void ToggleHand()
    {
        foreach (var card in cardsInHandUI)
        {
            if (card.IsCardFilled)
            {
                card.gameObject.SetActive(!isHandShown);
            }
        }
        if (isHandShown)
        {
            ui.ToggleCardsText.text = "Show Hand";
        }
        else
        {
            ui.ToggleCardsText.text = "Hide Hand";
        }
        isHandShown = !isHandShown;
    }

    public void AddCard(string card)
    {
        cards.Add(card);
        UpdateHandUI();
    }

    private void UpdateHandUI()
    {
        for (int i = 0; i < cards.Count; i++)
        {
            ui.ShowCardInHand(i, cards[i]);
        }
    }
}
