using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// The <c>GameLog</c> is used to print messages about the duel in the text box on top right corner of the screen.
/// </summary>
public class GameLog : MonoBehaviour
{
    static GameLog instance;
    static bool isShown;
    
    [SerializeField]
    Text logText;
    [SerializeField]
    ScrollRect scroll;
    [SerializeField]
    Button toggleLog;
    [SerializeField]
    Text toggleLogText;
    int turnCounter;

    private void Awake()
    {
        instance = this;
        isShown = true;
        turnCounter = 0;
    }

    public static void Log(GameObject sender, LogEvent logEvent)
    {
        switch (logEvent.Code)
        {
            case LogEventCode.PhaseChanged:
                if (logEvent.Details == GamePhases.Upkeep_Phase.GetLabel())
                {
                    instance.turnCounter++;
                    instance.logText.text += "==============\n";
                    instance.logText.text += "Turn " + instance.turnCounter.ToString() + "\n\n";
                }
                instance.logText.text += "Phase changed to: " + logEvent.Details + "\n\n";
                break;
            case LogEventCode.CardDrawnHand:
                if (sender.GetComponent<HumanPlayer>() != null)
                {
                    instance.logText.text += "You drew: " + logEvent.Details + "\n\n";
                }
                break;
            case LogEventCode.CardDrawnDiscard:
                if (sender.GetComponent<HumanPlayer>() != null)
                {
                    instance.logText.text += "You drew and discarded: " + logEvent.Details + "\n\n";
                }
                break;
            case LogEventCode.CardPlayed:
                if (sender.GetComponent<HumanPlayer>() != null)
                {
                    instance.logText.text += "You played: " + logEvent.Details + "\n\n";
                }
                else if (sender.GetComponent<AIPlayer>() != null)
                {
                    instance.logText.text += "The AI played: " + logEvent.Details + "\n\n";
                }
                break;
        }
        instance.ScrollToBottom();
    }

    IEnumerator scrollToBot()
    {
        yield return null;
        instance.scroll.verticalNormalizedPosition = 0;
    }

    void ScrollToBottom()
    {
        StartCoroutine(scrollToBot());
    }

    public void onToggleLogClick()
    {
        if (isShown)
        {
            isShown = false;
            toggleLogText.text = ">";
            gameObject.transform.position = transform.position - new Vector3(gameObject.GetComponent<RectTransform>().rect.width, 0, 0);
        }
        else
        {
            isShown = true;
            toggleLogText.text = "<";
            gameObject.transform.position = transform.position + new Vector3(gameObject.GetComponent<RectTransform>().rect.width, 0, 0);
        }
    }
}

/// <summary>
/// A <c>LogEvent</c> object contains the necessary information for the <c>GameLog</c> in order to print the appropriate message. 
/// </summary>
public class LogEvent
{
    LogEventCode code;
    string details;

    public string Details { get { return details; } }
    public LogEventCode Code { get { return code; } }

    public LogEvent(LogEventCode code, string details)
    {
        this.code = code;
        this.details = details;
    }
}

/// <summary>
/// An enum of the codes about the tpes of <c>LogEvent</c>s
/// </summary>
public enum LogEventCode
{
    PhaseChanged,
    CardDrawnHand,
    CardDrawnDiscard,
    CardPlayed,
}
