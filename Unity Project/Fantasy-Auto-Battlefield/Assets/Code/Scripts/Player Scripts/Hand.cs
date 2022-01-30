using System;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    [SerializeField]
    CardInHand[] cardsInHand;

    List<string> cards = new List<string>();

    bool isHandShown = true;
    public int CardsInHandCount { get { return cards.Count; } }

    public void AddCard(string card)
    {
        cards.Add(card);
        UpdateHandUI();
    }
    
    private void UpdateHandUI()
    {
        for (int cardIndex = 0; cardIndex < cards.Count; cardIndex++)
        {
            ShowCardInHand(cardIndex);
        }
    }
    
    public void ToggleHand()
    {
        MainUI ui = FindObjectOfType<MainUI>();
        if (isHandShown)
        {
            foreach (CardInHand card in cardsInHand)
            {
                card.HideSelf();
            }
        }
        else
        {
            foreach (CardInHand card in cardsInHand)
            {
                if (card.CardType != null)
                {
                    card.ShowSelf();
                }
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

    private void ShowCardInHand(int cardIndex)
    {
        string card = cards[cardIndex];

        switch (CardCatalog.GetType(card))
        {
            case CardType.Unit:
                cardsInHand[cardIndex].ShowUnit(UnitCardData.GetUnitDataFromName(card));
                break;
            case CardType.Building:
                cardsInHand[cardIndex].ShowBuilding(BuildingCardData.GetBuildingDataFromName(card));
                break;
            case CardType.Spell:
                cardsInHand[cardIndex].ShowSpell(SpellCardData.GetSpellDataFromName(card));
                break;
        }
    }
}
