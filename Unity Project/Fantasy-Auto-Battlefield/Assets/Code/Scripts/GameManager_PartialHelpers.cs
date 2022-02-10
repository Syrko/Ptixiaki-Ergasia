using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class GameManager
{
    public void onNextPhaseClick()
    {
        SubjectUI.Notify(this.gameObject, new UIEvent(EventUICodes.DISABLE_END_PHASE_BUTTON));
        SetPhase(currentPhase.NextPhase());
        ExecutePhaseProcess(currentPhase);
        SubjectUI.Notify(this.gameObject, new UIEvent(EventUICodes.ENABLE_END_PHASE_BUTTON));
    }

    public void onPlayCardClick()
    {
        SubjectUI.Notify(this.gameObject, new UIEvent(EventUICodes.DISABLE_END_PHASE_BUTTON));
        SubjectUI.Notify(this.gameObject, new UIEvent(EventUICodes.DISABLE_PLAY_BUTTON));
        player.Hand.HideUnselectedCards();
        HighlightFrontline();
    }

    void SwapInitiative()
    {
        if (player.HasInitiative)
        {
            player.HasInitiative = false;
            opponent.HasInitiative = true;
            // Send the game object that has the initiative
            SubjectUI.Notify(opponent.gameObject, new UIEvent(EventUICodes.INITIATIVE_TOKEN_SWAPPED));
        }
        else
        {
            player.HasInitiative = true;
            opponent.HasInitiative = false;
            // Send the game object that has the initiative
            SubjectUI.Notify(player.gameObject, new UIEvent(EventUICodes.INITIATIVE_TOKEN_SWAPPED));
        }
    }

    void SetPhase(GamePhases PhaseToSet)
    {
        currentPhase = PhaseToSet;
        SubjectUI.Notify(this.gameObject, new UIEvent(EventUICodes.PHASE_CHANGED, currentPhase.GetLabel()));
    }

    void HighlightFrontline()
    {
        for (int depth = 0; depth <= player.Frontline; depth++)
        {
            for (int width = 0; width < BoardWidth; width++)
            {
                if (Board[depth, width].GetComponent<HexTile>().OccupiedBy == null)
                {
                    Board[depth, width].GetComponent<HexTile>().Highlight(true);
                }
            }
        }
    }

    public GameObject GetSelectedCard()
    {
        return player.Hand.GetSelectedCardGameObject();
    }

    public void DeHighlightFrontline()
    {
        for (int depth = 0; depth <= player.Frontline; depth++)
        {
            for (int width = 0; width < BoardWidth; width++)
            {
                Board[depth, width].GetComponent<HexTile>().Highlight(false);
            }
        }
    }

    public void SpawnPawn(string cardName, int width, int depth, bool forPlayer)
    {
        CardType? cardType = CardCatalog.GetType(cardName);

        switch (cardType)
        {
            case CardType.Unit:
                Board[depth, width].GetComponent<HexTile>().OccupiedBy = UnitFactory.CreateUnitPawn(cardName, width, depth, forPlayer);
                break;
            case CardType.Building:
                Board[depth, width].GetComponent<HexTile>().OccupiedBy = BuildingFactory.CreateBuildingPawn(cardName, width, depth, forPlayer);
                break;
            case CardType.Spell:
                break;
            default:
                // TODO handle null
                break;
        }
    }

    public void PlayCard(CardInHand card, int x, int y, bool forPlayer)
    {
        player.PayMana(card.CardCost);
        SpawnPawn(card.CardName, x, y, forPlayer);
        SubjectUI.Notify(this.gameObject, new UIEvent(EventUICodes.ENABLE_END_PHASE_BUTTON));

        // Remove card from hand
        player.Hand.RemoveCardFromHand(card);
        CardInHand.cardIsBeingPlayed = false;
    }

    private void MoveAllUnits()
    {
        List<Unit> unitPawns = new List<Unit>();
        foreach (GameObject tile in Board)
        {
            GameObject occupant = tile.GetComponent<HexTile>().OccupiedBy;
            if(occupant == null)
                continue;

            Unit possibleUnit;
            if(occupant.TryGetComponent<Unit>(out possibleUnit))
            {
                unitPawns.Add(possibleUnit);
            }
        }

        foreach(Unit unit in unitPawns)
        {
            if (unit.Owner.HasInitiative)
            {
                //unit.Move();
            }
        }

        foreach (Unit unit in unitPawns)
        {
            if (!unit.Owner.HasInitiative)
            {
                //unit.Move();
            }
        }
    }
}
