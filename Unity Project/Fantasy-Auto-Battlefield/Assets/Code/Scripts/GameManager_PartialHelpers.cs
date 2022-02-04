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
        foreach (GameObject tile in Board)
        {
            tile.GetComponent<HexTile>().Highlight(true);
        }
        // TODO choose target
        // TODO pay cost
        // TODO remove card from hand
    }

    void SwapInitiative()
    {
        if (player.HasInitiative)
        {
            player.HasInitiative = false;
            // TODO remove comment
            //opponent.HasInitiative = true;
            SubjectUI.Notify(this.gameObject, new UIEvent(EventUICodes.INITIATIVE_TOKEN_SWAPPED));
        }
        else
        {
            player.HasInitiative = true;
            // TODO remove comment
            //opponent.HasInitiative = false;
            SubjectUI.Notify(this.gameObject, new UIEvent(EventUICodes.INITIATIVE_TOKEN_SWAPPED));
        }
    }

    void SetPhase(GamePhases PhaseToSet)
    {
        currentPhase = PhaseToSet;
        SubjectUI.Notify(this.gameObject, new UIEvent(EventUICodes.PHASE_CHANGED, currentPhase.GetLabel()));
    }
}
