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
        if (player.HasInitiative)
        {
            MovePlayerUnits();
            MoveAIUnits();
        }
        else if (opponent.HasInitiative)
        {
            MoveAIUnits();
            MovePlayerUnits();
        }
    }

    private void MovePlayerUnits()
    {
        for (int depth = BoardDepth - 1; depth >= 0; depth--)
        {
            for (int width = 0; width < BoardWidth; width++)
            {
                GameObject occupant = Board[depth, width].GetComponent<HexTile>().OccupiedBy;
                if (occupant != null)
                {
                    Unit possibleUnit;
                    if (occupant.TryGetComponent<Unit>(out possibleUnit))
                    {
                        if (possibleUnit.Owner is HumanPlayer)
                        {
                            if (depth + 1 > 0) // i.e if the forward hex is before the base of the opponent
                            {
                                if (Board[depth + 1, width].GetComponent<HexTile>().OccupiedBy == null)
                                {
                                    MovePlayerUnit(depth, width, possibleUnit);
                                }
                            }
                        }
                    }
                }
            }
        }
    }

    private void MoveAIUnits()
    {
        for (int depth = 0; depth < BoardDepth; depth++)
        {
            for (int width = 0; width < BoardWidth; width++)
            {
                GameObject occupant = Board[depth, width].GetComponent<HexTile>().OccupiedBy;
                if (occupant != null)
                {
                    Unit possibleUnit;
                    if (occupant.TryGetComponent<Unit>(out possibleUnit))
                    {
                        if (possibleUnit.Owner is AIPlayer)
                        {
                            if (depth - 1 > 0) // i.e if the forward hex is before the base of the opponent
                            {
                                if (Board[depth - 1, width].GetComponent<HexTile>().OccupiedBy == null)
                                {
                                    MoveAIUnit(depth, width, possibleUnit);
                                }
                            }
                        }
                    }
                }
            }
        }
    }

    private void MovePlayerUnit(int depth, int width, Unit possibleUnit)
    {
        possibleUnit.Move(new Vector2(depth, width));
        Board[depth, width].GetComponent<HexTile>().OccupiedBy = null;
        Board[depth + 1, width].GetComponent<HexTile>().OccupiedBy = possibleUnit.gameObject;
    }

    private void MoveAIUnit(int depth, int width, Unit possibleUnit)
    {
        possibleUnit.Move(new Vector2(depth, width));
        Board[depth, width].GetComponent<HexTile>().OccupiedBy = null;
        Board[depth - 1, width].GetComponent<HexTile>().OccupiedBy = possibleUnit.gameObject;
    }
}
