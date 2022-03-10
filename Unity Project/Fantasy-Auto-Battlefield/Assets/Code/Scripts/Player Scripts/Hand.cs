using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The <c>Hand</c> class represents the hand of the human player, i.e. contains their drawn cards.
/// </summary>
public class Hand : MonoBehaviour
{
    // Static index number to represent when the human player has no selected card
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

    /// <summary>
    /// Adds the given card to the hand
    /// </summary>
    /// <param name="card">The name of the card to add to the hand</param>
    public void AddCard(string card)
    {
        cards.Add(card);
        UpdateHandUI();
    }
    
    /// <summary>
    /// Updates the Hand UI by showing all the cards currently in hand
    /// </summary>
    private void UpdateHandUI()
    {
        for (int cardIndex = 0; cardIndex < cards.Count; cardIndex++)
        {
            ShowCardInHand(cardIndex);
        }
    }
    
    /// <summary>
    /// Hides and reveals the hand of the player in the UI
    /// </summary>
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

    /// <summary>
    /// Calls the necessary functions to assign values to the UI template that displays a card in the hand of the human player
    /// </summary>
    /// <param name="cardIndex"></param>
    private void ShowCardInHand(int cardIndex)
    {
        string card = cards[cardIndex];

        switch (CardCatalogue.GetType(card))
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

    /// <summary>
    /// Highlights the selected card on the UI and assigns its index to the <c>SelectedCardIndex</c>
    /// </summary>
    /// <param name="selectionIndex"></param>
    public void onCardSelection(int selectionIndex)
    {
        if(selectedCardIndex == selectionIndex)
        {
            selectedCardIndex = NO_CARD_SELECTED;
            if (gameManager.CurrentPhase == GamePhases.Standard_Phase)
            {
                SubjectUI.Notify(this.gameObject, new UIEvent(EventUICodes.DISABLE_PLAY_BUTTON));
                gameManager.DeHighlightBoard();
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

    /// <summary>
    /// Hides the unselected cards.
    /// Used when playing a card.
    /// </summary>
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

    /// <summary>
    /// Returns the gameobject of the selected card
    /// </summary>
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

    /// <summary>
    /// Removes a card from the hand and sends it to the <c>DiscardPile</c>.
    /// Used when playing a card.
    /// </summary>
    public void RemoveCardFromHandAndSendToDiscard(CardInHand card)
    {
        card.EmptyCardType();
        cards.Remove(card.CardName);
        selectedCardIndex = NO_CARD_SELECTED;
        UpdateHandUI();

        player.DiscardPile.AddCard(card.CardName);
    }
}
