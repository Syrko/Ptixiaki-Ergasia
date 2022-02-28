using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuButtons : MonoBehaviour
{
    [SerializeField]
    GameObject helpPage;

    [SerializeField]
    GameObject creditsPage;

    public void onExitClick()
    {
        Debug.Log("Exiting...");
        Application.Quit();
    }

    public void onPlayClick()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Gameplay");
    }

    public void onCreditClick()
    {
        creditsPage.SetActive(true);
    }

    public void onHelpClick()
    {
        helpPage.SetActive(true);
    }
}
