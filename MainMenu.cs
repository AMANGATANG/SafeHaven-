using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{

    public void StartGame()
    {
        Debug.Log("Game has started.");
        GameManager.Instance.GoToNextScene(1);
    }
    public void GoToSetting()
    {
        Debug.Log("Go to settings.");
    }
    public void ExitGame()
    {
        Debug.Log("Game has been exited.");
        Application.Quit();
    }
}
