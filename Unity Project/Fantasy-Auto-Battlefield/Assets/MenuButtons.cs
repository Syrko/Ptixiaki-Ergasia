using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButtons : MonoBehaviour
{
    public void onExitClick()
    {
        Application.Quit();
    }

    public void onPlayClick()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Gameplay");
    }

    public void onCreditClick()
    {

    }

    public void onHelpClick()
    {

    }
}
