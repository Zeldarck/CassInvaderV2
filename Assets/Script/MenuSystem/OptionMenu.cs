using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OptionMenu : Menu {

    [SerializeField]
    Slider m_ambiantSoundOption;

    [SerializeField]
    Slider m_sfxSoundOption;

    [SerializeField]
    Button m_mainMenu;


    protected override void Start()
    {
        base.Start();
        m_ambiantSoundOption.maxValue = SoundManager.INSTANCE.GetMixerVolume(MIXER_GROUP_TYPE.AMBIANT);
        m_ambiantSoundOption.onValueChanged.AddListener((float a_value) =>
        {
            SoundManager.INSTANCE.SetMixerVolume(MIXER_GROUP_TYPE.AMBIANT, a_value);
        });

        m_sfxSoundOption.maxValue = SoundManager.INSTANCE.GetMixerVolume(MIXER_GROUP_TYPE.SFX);
        m_sfxSoundOption.onValueChanged.AddListener((float a_value) =>
        {
            SoundManager.INSTANCE.SetMixerVolume(MIXER_GROUP_TYPE.SFX, a_value);
        });

        m_mainMenu.onClick.AddListener(() =>
       {
           MenuManager.INSTANCE.BackToMainMenu();
       });

    }

    public override void SetBackButtonAsClose()
    {
        base.SetBackButtonAsClose();
        m_mainMenu.gameObject.SetActive(true);
    }

    protected override void SetUpBackButtonListenner()
    {
        base.SetUpBackButtonListenner();
        m_mainMenu.gameObject.SetActive(false);
    }

}
