using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameLog : MonoBehaviour
{
    static GameLog instance;
    
    Text logText;
    int turnCounter;

    private void Awake()
    {
        instance = this;
        turnCounter = 0;
    }

    public static void Log(GameObject sender, LogEvent logEvent)
    {
        switch (logEvent.Code)
        {
            case LogEventCode.PhaseChanged:
                if (logEvent.Details == "Upkeep")
                {
                    instance.turnCounter++;
                    instance.logText.text = "==============\n";
                    instance.logText.text = "Turn " + instance.turnCounter.ToString() + "\n";
                }
                instance.logText.text += "Phase changed to: " + logEvent.Details + '\n';
                break;
            case LogEventCode.CardDrawn:
                if (sender.GetComponent<HumanPlayer>() != null)
                {
                    instance.logText.text += "You drew: " + logEvent.Details + '\n';
                }
                break;
            case LogEventCode.CardPlayed:
                if (sender.GetComponent<HumanPlayer>() != null)
                {
                    instance.logText.text += "You played: " + logEvent.Details + '\n';
                }
                break;
        }
    }
}

public class LogEvent
{
    LogEventCode code;
    string details;

    public string Details { get { return details; } }
    public LogEventCode Code { get { return code; } }
}

public enum LogEventCode
{
    PhaseChanged,
    CardDrawn,
    CardPlayed,
}
