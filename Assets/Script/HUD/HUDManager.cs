using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class HUDManager : Singleton<HUDManager> {


    [SerializeField]
    Text m_scoreText;

    [SerializeField]
    Button m_pauseButton;


    void Start()
    {
        m_pauseButton.onClick.AddListener(() =>
        {
            Menu pause = MenuManager.INSTANCE.OpenMenu(MENUTYPE.OPTION);
            pause.GetComponent<OptionMenu>().SetBackButtonAsClose();
            SoundManager.INSTANCE.StartAudio(AUDIOCLIP_KEY.BUTTON_MENU, MIXER_GROUP_TYPE.SFX_MENU, false, false, AUDIOSOURCE_KEY.NO_KEY_AUTODESTROY);
        });
    }
    // Set Display Time
    public void SetScore(int a_score){
        m_scoreText.text = a_score +"";
	}
}
