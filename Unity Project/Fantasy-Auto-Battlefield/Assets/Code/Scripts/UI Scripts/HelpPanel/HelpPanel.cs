using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HelpPanel : MonoBehaviour
{
    [SerializeField]
    Image imageHelp;
    [SerializeField]
    TextMeshProUGUI textHelp;
    [SerializeField]
    TextMeshProUGUI counterHelp;
    [SerializeField]
    Button nextHelp;
    [SerializeField]
    Button previousHelp;
    [SerializeField]
    HelpPanelEntry[] helpEntries;

    int currentHelpEntry;

    private void Awake()
    {
        currentHelpEntry = 0;
    }

    private void Start()
    {
        UpdateHelp();
    }

    public void onNextHelpClick()
    {
        if (currentHelpEntry + 1 < helpEntries.Length)
        {
            currentHelpEntry += 1;
            UpdateHelp();
        }
    }

    public void onPreviousHelpClick()
    {
        if (currentHelpEntry - 1 >= 0)
        {
            currentHelpEntry -= 1;
            UpdateHelp();
        }
    }

    public void onExitHelpClick()
    {
        this.gameObject.SetActive(false);
    }

    private void UpdateHelp()
    {
        imageHelp.sprite = helpEntries[currentHelpEntry].HelpImage;
        textHelp.text = helpEntries[currentHelpEntry].HelpText;

        counterHelp.text = (currentHelpEntry + 1).ToString() + "/" + helpEntries.Length.ToString();
    }
}
