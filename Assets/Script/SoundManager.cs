using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Audio;


public enum RANDOM_SOUND_TYPE {  };

[System.Serializable]
public class RandomSound
{
    [SerializeField]
    RANDOM_SOUND_TYPE m_type;
    [SerializeField]
    List<AudioClip> m_listSounds;
    List<AudioClip> m_everUsed;

    public List<AudioClip> ListSounds
    {
        get
        {
            return m_listSounds;
        }

        private set
        {
            m_listSounds = value;
        }
    }

    public RANDOM_SOUND_TYPE Type
    {
        get
        {
            return m_type;
        }

        private set
        {
            m_type = value;
        }
    }

    public List<AudioClip> EverUsed
    {
        get
        {
            return m_everUsed;
        }

        private set
        {
            m_everUsed = value;
        }
    }
}

public enum MIXER_GROUP_TYPE { AMBIANT, SFX_MENU, SFX_GOOD, SFX_BAD };

[System.Serializable]
public class MixerGroupLink
{
    [SerializeField]
    MIXER_GROUP_TYPE m_mixerType;
    [SerializeField]
    AudioMixerGroup m_mixerGroup;

    public MIXER_GROUP_TYPE MixerType
    {
        get
        {
            return m_mixerType;
        }

        private set
        {
            m_mixerType = value;
        }
    }

    public AudioMixerGroup MixerGroup
    {
        get
        {
            return m_mixerGroup;
        }

        private set
        {
            m_mixerGroup = value;
        }
    }
}



public class SoundManager : Singleton<SoundManager>
{

    [SerializeField]
    List<MixerGroupLink> m_listMixerGroup;

    [SerializeField]
    List<RandomSound> m_listRandomSound;




    private AudioSource CreateAudioSource(AudioMixerGroup a_mixerGroup)
    {
        AudioSource res;
        res = gameObject.AddComponent<AudioSource>();
        res.loop = true;
        res.Stop();
        res.outputAudioMixerGroup = a_mixerGroup;
        return res;
    }
    

    public void StartAudio(AudioClip a_clip, MIXER_GROUP_TYPE a_mixerGroupType = MIXER_GROUP_TYPE.AMBIANT, bool a_isLooping = true)
    {
        try
        {
            AudioMixerGroup mixer = m_listMixerGroup.Find(x => x.MixerType == a_mixerGroupType).MixerGroup;
            AudioSource new_source = CreateAudioSource(mixer);
            new_source.clip = a_clip;
            new_source.loop = a_isLooping;
            new_source.Play();
            Debug.Log("StartAudio : " + a_clip.name);
        }
        catch
        {
            Debug.LogError("No mixerGroup of type " + a_mixerGroupType);
        }
    }


    public void StartRandom(RANDOM_SOUND_TYPE a_randomSoundType, MIXER_GROUP_TYPE a_mixerGroupType)
    {

        RandomSound randomSound = m_listRandomSound.Find(x => x.Type == a_randomSoundType);
        if (randomSound.ListSounds.Count > 0)
        {
            int rnd = Random.Range(0, randomSound.ListSounds.Count);
            AudioClip toPlay = randomSound.ListSounds[rnd];
            StartAudio(randomSound.ListSounds[rnd], a_mixerGroupType, false);
            randomSound.EverUsed.Add(toPlay);
            randomSound.ListSounds.Remove(toPlay);

            if (randomSound.ListSounds.Count == 0)
            {
                randomSound.ListSounds.AddRange(randomSound.EverUsed);
                randomSound.EverUsed.Clear();
            }
        }

    }
}
