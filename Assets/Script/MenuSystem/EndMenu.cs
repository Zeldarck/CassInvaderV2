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

    
    public override void OnOpen()
    {
        Clean();
        /*
        WordData level = GameManager.Instance.CurrentLevel;
        m_wordText.text = level + "";
        WordData nextLevel = DataManager.Instance.GetNextWordData(level);
        if (nextLevel == null)
        {
            m_nextButton.gameObject.SetActive(false);
        }
        else
        {
            m_nextButton.onClick.AddListener(() =>
            {
                GameManager.Instance.BeginGame(nextLevel);
            });
        }
        */
    }

    void Clean()
    {
        /*
        m_nextButton.gameObject.SetActive(true);
        m_nextButton.onClick.RemoveAllListeners();
        */
    }

    public void OnBackButton()
    {
        MenuManager.INSTANCE.BackToMainMenu();
    }
}