using System;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    public static readonly int NO_CARD_SELECTED = -1;
    [SerializeField]
    GameManager gameManager;
    [SerializeField]
    CardInHand[] cardsInHand;

    List<string> cards = new List<string>();

    bool isHandShown = true;
    int selectedCardIndex = NO_CARD_SELECTED;

    public int SelectedCardIndex { get => selectedCardIndex; }
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
                cardsInHand[cardIndex].DrawUnit(UnitCardData.GetUnitDataFromName(card), isHandShown);
                break;
            case CardType.Building:
                cardsInHand[cardIndex].DrawBuilding(BuildingCardData.GetBuildingDataFromName(card), isHandShown);
                break;
            case CardType.Spell:
                cardsInHand[cardIndex].DrawSpell(SpellCardData.GetSpellDataFromName(card), isHandShown);
                break;
        }

        bool isCardSelected = cardIndex == selectedCardIndex;
        cardsInHand[cardIndex].UpdateBorder(isCardSelected);
    }

    public void onCardSelection(int selectionIndex)
    {
        if(selectedCardIndex == selectionIndex)
        {
            selectedCardIndex = NO_CARD_SELECTED;
            if (gameManager.CurrentPhase == GamePhases.Standard_Phase)
            {
                SubjectUI.Notify(this.gameObject, new UIEvent(EventUICodes.DISABLE_PLAY_BUTTON));
            }
        }
        else
        {
            selectedCardIndex = selectionIndex;
            if(gameManager.CurrentPhase == GamePhases.Standard_Phase)
            {
                if (cardsInHand[selectedCardIndex].IsPlayable)
                {
                    SubjectUI.Notify(this.gameObject, new UIEvent(EventUICodes.ENABLE_PLAY_BUTTON));
                }
            }
        }
        UpdateHandUI();
    }
}
