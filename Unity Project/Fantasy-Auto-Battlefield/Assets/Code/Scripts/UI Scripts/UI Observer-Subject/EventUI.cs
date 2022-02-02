using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventUI
{
    EventUICodes code;
    string value;

    public EventUICodes Code { get => code; }
    public string Value { get => value; }

    public EventUI(EventUICodes code, string value)
    {
        this.code = code;
        this.value = value;
    }
    
    public EventUI(EventUICodes code)
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
    TOGGLE_PLAY_BUTTON,
    TOGGLE_HAND_BUTTON,
    TOGGLE_MENU_BUTTON,
    TOGGLE_END_PHASE_BUTTON
}