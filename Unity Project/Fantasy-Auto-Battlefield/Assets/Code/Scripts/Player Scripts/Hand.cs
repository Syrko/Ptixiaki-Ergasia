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
    [SerializeField]
    HumanPlayer player;

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
            ui.ToggleCardsText.text = "Show Hand";
        }
        else
        {
            ui.ToggleCardsText.text = "Hide Hand";
        }

        if (isHandShown)
        {
            foreach (CardInHand card in cardsInHand)
            {
                card.HideSelf();
                isHandShown = !isHandShown;
            }
        }
        else
        {
            foreach (CardInHand card in cardsInHand)
            {
                UpdateHandUI();
                isHandShown = !isHandShown;
            }
        }
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
                gameManager.DeHighlightFrontline();
                SubjectUI.Notify(this.gameObject, new UIEvent(EventUICodes.ENABLE_END_PHASE_BUTTON));
            }
        }
        else
        {
            selectedCardIndex = selectionIndex;
            if(gameManager.CurrentPhase == GamePhases.Standard_Phase)
            {
                if (cardsInHand[selectedCardIndex].CardCost <= player.CurrentMana)
                {
                    SubjectUI.Notify(this.gameObject, new UIEvent(EventUICodes.ENABLE_PLAY_BUTTON));
                }
            }
        }
        UpdateHandUI();
    }

    public void HideUnselectedCards()
    {
        for (int index = 0; index < cardsInHand.Length; index++)
        {
            if(index == selectedCardIndex)
            {
                continue;
            }
            cardsInHand[index].HideSelf();
        }
    }

    public GameObject GetSelectedCardGameObject()
    {
        try
        {
            return cardsInHand[selectedCardIndex].gameObject;
        }
        catch (IndexOutOfRangeException e)
        {
            Debug.LogException(e);
            return null;
        }
    }

    public void RemoveCardFromHand(CardInHand card)
    {
        card.EmptyCardType();
        cards.Remove(card.CardName);
        selectedCardIndex = NO_CARD_SELECTED;
        UpdateHandUI();
    }
}
