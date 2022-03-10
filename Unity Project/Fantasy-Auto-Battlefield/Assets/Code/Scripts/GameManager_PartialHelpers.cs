using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This is a partial file of the GameManager class that contains essential methods
// The main file only contains the initialization of the game state and the basic logic of the game phases.
// Here are the methods the main file uses to implement its functionality.
public partial class GameManager
{
    /// <summary>
    /// Method for the Next Phase button
    /// </summary>
    public void onNextPhaseClick()
    {
        SetPhase(currentPhase.NextPhase());
        ExecutePhaseProcess(currentPhase);
    }

    /// <summary>
    /// Method for the Play Card button
    /// </summary>
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

    /// <summary>
    /// Swaps the initiative between the two players.
    /// </summary>
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

    /// <summary>
    /// Changes the current phase and updates the UI
    /// </summary>
    void SetPhase(GamePhases PhaseToSet)
    {
        currentPhase = PhaseToSet;
        SubjectUI.Notify(this.gameObject, new UIEvent(EventUICodes.PHASE_CHANGED, currentPhase.GetLabel()));
    }

    /// <summary>
    /// Highlights the hexes that are behind the frontline of the player
    /// </summary>
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

    /// <summary>
    /// Highlights the hexes that are occupied by pawns
    /// </summary>
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

    /// <summary>
    /// Turns off the highlight component of all the highlighted hexes
    /// </summary>
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

    /// <summary>
    /// This method creates a new pawn of the given card, at the specified coorindates.
    /// </summary>
    /// <param name="cardName">The name of the unit or building you want to create</param>
    /// <param name="width">The width coordinate on the array board</param>
    /// <param name="depth">The depth coordinate on the array board</param>
    /// <param name="forPlayer">Set as true, if the owner of the pawn is the human player</param>
    public void SpawnPawn(string cardName, int width, int depth, bool forPlayer)
    {
        CardType? cardType = CardCatalogue.GetType(cardName);
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

    /// <summary>
    /// This method is called when a Unit or Building card is being played.
    /// </summary>
    /// <param name="card">The <c>CardInHand</c> object of the card that is being played</param>
    /// <param name="x">The width coordinate on the array board</param>
    /// <param name="y">The depth coordinate on the array board</param>
    /// <param name="forPlayer">Set as true, if the owner of the pawn is the human player</param>
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

    /// <summary>
    /// This method is called when a Spell card is being played.
    /// </summary>
    /// <param name="card">The <c>CardInHand</c> object of the card that is being played</param>
    /// <param name="x">The width coordinate on the array board</param>
    /// <param name="y">The depth coordinate on the array board</param>
    /// <param name="forPlayer">Set as true, if the owner of the pawn is the human player</param>
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

    /// <summary>
    /// This methods orders the movement of all the units on the board.
    /// It takes into account which player has the initiative and moves their pawns first.
    /// </summary>
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

    /// <summary>
    /// This method orders the move of all the units of the Human Player that are on the board
    /// </summary>
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

    /// <summary>
    /// This method orders the move of all the units of the AI Player that are on the board
    /// </summary>
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

    /// <summary>
    /// This method orders the move of an individual unit of the Human Player.
    /// After that it updates its position on the array board.
    /// </summary>
    private void MovePlayerUnit(int depth, int width, Unit possibleUnit)
    {
        possibleUnit.Move(new Vector2Int(depth, width));
        Board[depth, width].GetComponent<HexTile>().OccupiedBy = null;
        Board[depth + 1, width].GetComponent<HexTile>().OccupiedBy = possibleUnit.gameObject;
    }

    /// <summary>
    /// This method orders the move of an individual unit of the AI Player.
    /// After that it updates its position on the array board.
    /// </summary>
    private void MoveAIUnit(int depth, int width, Unit possibleUnit)
    {
        possibleUnit.Move(new Vector2Int(depth, width));
        Board[depth, width].GetComponent<HexTile>().OccupiedBy = null;
        Board[depth - 1, width].GetComponent<HexTile>().OccupiedBy = possibleUnit.gameObject;
    }

    /// <summary>
    /// This method orders every unit on the board to try and attack
    /// </summary>
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

    /// <summary>
    /// This is a method to execute the terrain effects of the desert and lake tiles,
    /// i.e. those that can be iterated through the board and the effect stays the same.
    /// </summary>
    public void ExecuteTerrainEffects()
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

    /// <summary>
    /// This method is used to execute the effects of hills and forests, i.e the tile types that change pawn stats.
    /// For those effects coordinates and the pawn is required, in order to determine if the pawn enters or leaves a tile of those types,
    /// and to change its effects
    /// </summary>
    /// <param name="depth">The depth coordinate on the array board</param>
    /// <param name="width">The width coordinate on the array board</param>
    /// <param name="pawn">The pawn that moves</param>
    /// <param name="isPawnEntering">Set as true if the pawn is entering a new tile that is hills or forest, and false when it's leaving</param>
    public void ExecuteTerrainEffects(int depth, int width, GameObject pawn, bool isPawnEntering)
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
