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
    }


}
