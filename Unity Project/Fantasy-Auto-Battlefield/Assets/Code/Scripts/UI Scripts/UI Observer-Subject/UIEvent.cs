using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIEvent
{
    EventUICodes code;
    string value;

    public EventUICodes Code { get => code; }
    public string Value { get => value; }

    public UIEvent(EventUICodes code, string value)
    {
        this.code = code;
        this.value = value;
    }
    
    public UIEvent(EventUICodes code)
    {
        this.code = code;
        value = string.Empty;
    }
}

public enum EventUICodes
{
    PLAYER_MANA_CHANGED,
    PLAYER_HP_CHANGED,
    OPPONENT_HP_CHANGED,
    PHASE_CHANGED,
    CARD_INFO_CHANGED,
    TILE_INFO_CHANGED,
    LOG_ENTRY,
    DECK_COUNTER_CHANGED,
    INITIATIVE_TOKEN_SWAPPED,
    ENABLE_PLAY_BUTTON,
    DISABLE_PLAY_BUTTON,
    ENABLE_END_PHASE_BUTTON,
    DISABLE_END_PHASE_BUTTON
}