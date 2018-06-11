using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Audio;
using UnityEngine.Assertions;

public enum RANDOM_SOUND_TYPE { };

[System.Serializable]
public class RandomSound
{
    [SerializeField]
    RANDOM_SOUND_TYPE m_type;
    [SerializeField]
    List<AudioClip> m_listSounds;
    List<AudioClip> m_everUsed;
    int m_audioSourceKey = (int)AUDIOSOURCE_KEY.CREATE_KEY;

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

    public int AudioSourceKey
    {
        get
        {
            return m_audioSourceKey;
        }

        set
        {
            m_audioSourceKey = value;
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

public enum AUDIOSOURCE_KEY { NO_KEY_AUTODESTROY, CREATE_KEY };



public class SoundManager : Singleton<SoundManager>
{

    [SerializeField]
    List<MixerGroupLink> m_listMixerGroup;

    [SerializeField]
    List<RandomSound> m_listRandomSound;

    Dictionary<int, AudioSource> m_audioSources = new Dictionary<int, AudioSource>();
    List<AudioSource> m_autoDestroyAudioSources = new List<AudioSource>();
    int m_maxAllocatedKey = (int)AUDIOSOURCE_KEY.CREATE_KEY;
 


    public bool IsAudioPlaying(int a_key)
    {
        bool res = false;
        AudioSource output;
        if(m_audioSources.TryGetValue(a_key, out output))
        {
            res = output.isPlaying;
        }

        return res;
    }


    AudioSource GetAudioSource(int a_key)
    {
        AudioSource res;
        if (!m_audioSources.TryGetValue(a_key, out res))
        {
            res = CreateAudioSource();
            if(a_key != (int)AUDIOSOURCE_KEY.NO_KEY_AUTODESTROY)
            {
                m_audioSources.Add(a_key, res);
            }
            else
            {
                m_autoDestroyAudioSources.Add(res);
            }
        }

        return res;
    }

    AudioSource CreateAudioSource()
    {
        AudioSource res;
        res = gameObject.AddComponent<AudioSource>();
        res.loop = true;
        res.Stop();
        return res;
    }

    int GetKey(int a_key)
    {
        Assert.IsFalse(a_key < 0 || a_key > m_maxAllocatedKey, "Bad sound key");
  
        if (a_key == (int)AUDIOSOURCE_KEY.CREATE_KEY)
        {
            a_key = ++m_maxAllocatedKey;
        }
        return a_key;
    }

    public int GenerateKey()
    {
        return GetKey((int)AUDIOSOURCE_KEY.CREATE_KEY);
    }
    //TODO Make Fade if id ever exist, Destroy auto destroy audiosources, see to move initialisation of audiosource elsewhere

    public int StartAudio(AudioClip a_clip, MIXER_GROUP_TYPE a_mixerGroupType = MIXER_GROUP_TYPE.AMBIANT, bool a_isLooping = true, int a_key = (int)AUDIOSOURCE_KEY.CREATE_KEY, ulong a_delay = 0)
    {
        int res = -1;
        res = GetKey(a_key);
        try
        {
            AudioMixerGroup mixer = m_listMixerGroup.Find(x => x.MixerType == a_mixerGroupType).MixerGroup;
            AudioSource new_source = GetAudioSource(a_key);
            new_source.outputAudioMixerGroup = mixer;
            new_source.clip = a_clip;
            new_source.loop = a_isLooping;
            new_source.Play(0);
            Debug.Log("StartAudio : " + a_clip.name);
        }
        catch
        {
            Debug.LogError("Error when try to launch sound");
        }
        return res;
    }


    public void StopAudio(int a_key = (int)AUDIOSOURCE_KEY.CREATE_KEY)
    {
        AudioSource audioSource;
        if (INSTANCE.m_audioSources.TryGetValue(a_key, out audioSource))
        {
            audioSource.Stop();
            audioSource.clip = null;
            Debug.Log("StopAudio : " + a_key);
        }
    }

    public void StartRandom(RANDOM_SOUND_TYPE a_randomSoundType, MIXER_GROUP_TYPE a_mixerGroupType)
    {

        RandomSound randomSound = m_listRandomSound.Find(x => x.Type == a_randomSoundType);
        if (randomSound.ListSounds.Count > 0)
        {
            int rnd = Random.Range(0, randomSound.ListSounds.Count);
            AudioClip toPlay = randomSound.ListSounds[rnd];
            randomSound.AudioSourceKey = StartAudio(randomSound.ListSounds[rnd], a_mixerGroupType, false, randomSound.AudioSourceKey);
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
