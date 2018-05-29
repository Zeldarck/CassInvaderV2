using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartMenu : Menu {

    [SerializeField]
    private Button m_creditsButton;

    private void Start()
    {
        m_creditsButton.onClick.AddListener(() =>
        {
            MenuManager.INSTANCE.OpenMenu(MENUTYPE.CREDITS);
        });
    }

    public void OnPlayButton()
    {
        GameManager.INSTANCE.StartGame();
    }
}
