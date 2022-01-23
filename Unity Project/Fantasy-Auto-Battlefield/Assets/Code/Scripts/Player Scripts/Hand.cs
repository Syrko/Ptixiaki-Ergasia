using System;
using System.Collections.Generic;
using UnityEngine;

public class Hand
{
    List<string> cards;

    UIManager ui;
    CardTemplateUI[] cardsInHandUI;

    bool isHandShown;

    public Hand()
    {
        cards = new List<string>();
        ui = GameObject.FindObjectOfType<UIManager>();
        cardsInHandUI = GameObject.FindObjectsOfType<CardTemplateUI>(true);
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
