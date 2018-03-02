using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Menu : MonoBehaviour
{

    public void StartGame(bool a_vsIA = false)
    {
        GameManager.StartGame(a_vsIA);
    }

    public void RestartGame()
    {
        GameManager.RestartGame();
    }

    public void CloseMenu()
    {
        MenuManager.CloseMenu();
    }

    /// <summary>
    /// If true the background of menu is displayed
    /// </summary>
    public virtual bool DisplayBack()
    {
        return true;
    }

    public void OpenMainMenu()
    {
        MenuManager.OpenMenu(MENUTYPE.MAIN);
    }

}