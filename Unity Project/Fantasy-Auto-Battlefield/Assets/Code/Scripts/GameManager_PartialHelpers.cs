using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class GameManager
{
    public void onNextPhaseClick()
    {
        SetPhase(currentPhase.NextPhase());
        ExecutePhaseProcess(currentPhase);
    }

    public void onPlayCardClick()
    {
        SubjectUI.Notify(this.gameObject, new UIEvent(EventUICodes.DISABLE_END_PHASE_BUTTON));
        SubjectUI.Notify(this.gameObject, new UIEvent(EventUICodes.DISABLE_PLAY_BUTTON));
        SubjectUI.Notify(this.gameObject, new UIEvent(EventUICodes.DISABLE_TOGGLE_HAND_BUTTON));
        player.Hand.HideUnselectedCards();

        CardInHand playedCard = player.Hand.GetSelectedCardGameObject().GetComponent<CardInHand>();
        if (playedCard.CardType == CardType.Spell)
        {
            HighlightPawns();
        }
        else
        {
            HighlightFrontline();
        }
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

    void HighlightPawns()
    {
        for (int depth = 0; depth < BoardDepth; depth++)
        {
            for (int width = 0; width < BoardWidth; width++)
            {
                if (Board[depth, width].GetComponent<HexTile>().OccupiedBy != null)
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

    public void DeHighlightBoard()
    {
        for (int depth = 0; depth < BoardDepth; depth++)
        {
            for (int width = 0; width < BoardWidth; width++)
            {
                Board[depth, width].GetComponent<HexTile>().Highlight(false);
            }
        }
        if (!CardInHand.cardIsBeingPlayed)
            SubjectUI.Notify(this.gameObject, new UIEvent(EventUICodes.ENABLE_TOGGLE_HAND_BUTTON));
    }

    public void SpawnPawn(string cardName, int width, int depth, bool forPlayer)
    {
        CardType? cardType = CardCatalog.GetType(cardName);
        GameObject pawn;

        switch (cardType)
        {
            case CardType.Unit:
                pawn = UnitFactory.CreateUnitPawn(cardName, width, depth, forPlayer);
                Board[depth, width].GetComponent<HexTile>().OccupiedBy = pawn;
                ExecuteTerrainEffects(depth, width, pawn ,true);
                break;
            case CardType.Building:
                pawn = BuildingFactory.CreateBuildingPawn(cardName, width, depth, forPlayer);
                Board[depth, width].GetComponent<HexTile>().OccupiedBy = pawn;
                ExecuteTerrainEffects(depth, width, pawn, true);
                break;
            case CardType.Spell:
                break;
        }
    }

    public void PlaySpawnableCard(CardInHand card, int x, int y, bool forPlayer)
    {
        player.PayMana(card.CardCost);
        SpawnPawn(card.CardName, x, y, forPlayer);
        SubjectUI.Notify(this.gameObject, new UIEvent(EventUICodes.ENABLE_END_PHASE_BUTTON));

        // Remove card from hand
        player.Hand.RemoveCardFromHandAndSendToDiscard(card);
        SubjectUI.Notify(this.gameObject, new UIEvent(EventUICodes.ENABLE_TOGGLE_HAND_BUTTON));
        CardInHand.cardIsBeingPlayed = false;
    }

    public void PlaySpellCard(CardInHand card, int x, int y)
    {
        player.PayMana(card.CardCost);
        SpellFactory.ActivateSpell(card.CardName, x, y);
        SubjectUI.Notify(this.gameObject, new UIEvent(EventUICodes.ENABLE_END_PHASE_BUTTON));

        // Remove card from hand
        player.Hand.RemoveCardFromHandAndSendToDiscard(card);
        SubjectUI.Notify(this.gameObject, new UIEvent(EventUICodes.ENABLE_TOGGLE_HAND_BUTTON));
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
                            if (depth + 1 < BoardDepth - 1) // i.e if the forward hex is before the base of the opponent
                            {
                                if (Board[depth + 1, width].GetComponent<HexTile>().OccupiedBy == null)
                                {
                                    MovePlayerUnit(depth, width, possibleUnit);
                                    ExecuteTerrainEffects(depth, width, possibleUnit.gameObject, false);    // Execute terrain effect for leaving the old tile 
                                    ExecuteTerrainEffects(depth + 1, width, possibleUnit.gameObject, true); // Execute terrain effect for entering the new tile
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
                                    ExecuteTerrainEffects(depth, width, possibleUnit.gameObject, false);    // Execute terrain effect for leaving the old tile 
                                    ExecuteTerrainEffects(depth - 1, width, possibleUnit.gameObject, true); // Execute terrain effect for entering the new tile
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
        possibleUnit.Move(new Vector2Int(depth, width));
        Board[depth, width].GetComponent<HexTile>().OccupiedBy = null;
        Board[depth + 1, width].GetComponent<HexTile>().OccupiedBy = possibleUnit.gameObject;
    }

    private void MoveAIUnit(int depth, int width, Unit possibleUnit)
    {
        possibleUnit.Move(new Vector2Int(depth, width));
        Board[depth, width].GetComponent<HexTile>().OccupiedBy = null;
        Board[depth - 1, width].GetComponent<HexTile>().OccupiedBy = possibleUnit.gameObject;
    }

    private void AllUnitsAttack()
    {
        for (int depth = 0; depth < BoardDepth; depth++)
        {
            for (int width = 0; width < BoardWidth; width++)
            {
                GameObject occupant = Board[depth, width].GetComponent<HexTile>().OccupiedBy;
                if (occupant != null)
                {
                    Vector2Int attackerPos = new Vector2Int(depth, width);
                    occupant.GetComponent<Spawnable>().Attack(Board, attackerPos);
                }
            }
        }
    }

    private void ExecuteTerrainEffects()
    {
        for (int depth = 0; depth < BoardDepth; depth++)
        {
            for (int width = 0; width < BoardWidth; width++)
            {
                HexTile hex = Board[depth, width].GetComponent<HexTile>();
                GameObject occupant = hex.OccupiedBy;
                if (occupant != null)
                {
                    switch (hex.TileType)
                    {
                        case TileType.Lake:
                            occupant.GetComponent<Spawnable>().HealDamage(HexTile.TerrainEffectMagnitude);
                            Instantiate(HexTile.TerrainEffectFX, occupant.transform);
                            break;
                        case TileType.Desert:
                            occupant.GetComponent<Spawnable>().TakeDamage(HexTile.TerrainEffectMagnitude);
                            Instantiate(HexTile.TerrainEffectFX, occupant.transform);
                            break;
                    }
                }
            }
        }
    }

    private void ExecuteTerrainEffects(int depth, int width, GameObject pawn, bool isPawnEntering)
    {
        HexTile hex = Board[depth, width].GetComponent<HexTile>();

        switch (hex.TileType)
        {
            case TileType.Forest:
                if(isPawnEntering)
                    pawn.GetComponent<Spawnable>().IncreaseDefense(HexTile.TerrainEffectMagnitude);
                else
                    pawn.GetComponent<Spawnable>().DecreaseDefense(HexTile.TerrainEffectMagnitude);
                break;
            case TileType.Hills:
                if (isPawnEntering)
                    pawn.GetComponent<Spawnable>().IncreaseAttack(HexTile.TerrainEffectMagnitude);
                else
                    pawn.GetComponent<Spawnable>().DecreaseAttack(HexTile.TerrainEffectMagnitude);
                break;
        }
    }
}
