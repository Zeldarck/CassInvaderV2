using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartMenu : Menu {

    [SerializeField]
    private Button m_creditsButton;

    [SerializeField]
    AudioClip m_backGroundMusic;

    private void Start()
    {
        m_creditsButton.onClick.AddListener(() =>
        {
            MenuManager.INSTANCE.OpenMenu(MENUTYPE.CREDITS);
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
