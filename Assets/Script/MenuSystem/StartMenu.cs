using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartMenu : Menu {

    [SerializeField]
    private Button m_creditsButton;

    [SerializeField]
    Button m_quitButton;

    [SerializeField]
    Button m_optionButton;

    [SerializeField]
    AudioClip m_backGroundMusic;

    protected override void Start()
    {
        base.Start();
        m_creditsButton.onClick.AddListener(() =>
        {
            MenuManager.INSTANCE.OpenMenu(MENUTYPE.CREDITS);
        });

        m_optionButton.onClick.AddListener(() =>
        {
            MenuManager.INSTANCE.OpenMenu(MENUTYPE.OPTION);
        });


        m_quitButton.onClick.AddListener(() =>
        {
            Utils.QuitApp();
        });

    }

    public override void OnOpen()
    {
        base.OnOpen();
        SoundManager.INSTANCE.StartAudio(m_backGroundMusic, MIXER_GROUP_TYPE.AMBIANT, true, true, AUDIOSOURCE_KEY.BACKGROUND);
    }


    public void OnPlayButton()
    {
        GameManager.INSTANCE.StartGame();
    }
}
