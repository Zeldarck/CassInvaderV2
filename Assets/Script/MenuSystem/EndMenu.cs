using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndMenu : Menu
{
    [SerializeField]
    Text m_wordText;

    [SerializeField]
    Button m_menuButton;

    [SerializeField]
    Button m_restartButton;

    public override void OnOpen()
    {
        string endText;
        int level = EnemyManager.INSTANCE.GetCurrentLevel();
        int score = GameManager.INSTANCE.PlayerScore;

        if (EnemyManager.INSTANCE.GetCurrentLevel() >= EnemyManager.INSTANCE.GetMaxLevel())
        {
            endText = "That's a win !";
        }

        else
        {
            endText = "Noob !!";
        }

        m_wordText.text = string.Concat(endText, "\n Current level : ", level, "\n Score : ", score);
    }

    public void OnBackButton()
    {
        MenuManager.INSTANCE.BackToMainMenu();
    }

    public void OnRestartButton()
    {
        GameManager.INSTANCE.StartGame();
    }

}