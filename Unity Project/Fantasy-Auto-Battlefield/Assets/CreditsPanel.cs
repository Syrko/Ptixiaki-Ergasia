using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CreditsPanel : MonoBehaviour
{
    [SerializeField]
    TMP_InputField creditsText;
    [SerializeField, TextArea(20, int.MaxValue)]
    string credits;

    // Start is called before the first frame update
    void Start()
    {
        creditsText.text = credits;
    }

    public void onExitClick()
    {
        this.gameObject.SetActive(false);
    }
}
