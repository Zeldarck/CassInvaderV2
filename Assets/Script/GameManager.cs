using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;
using UnityEngine.UI;


public class GameManager : Singleton<GameManager> {

    /// <summary>
    /// Score of player 1
    /// </summary>
    int m_playerScore = 0;

    bool m_isGameRunning = false;

    bool m_isGameWon;

    [SerializeField]
    AudioClip m_backGroundMusic;

    public int PlayerScore
    {
        get
        {
            return m_playerScore;
        }
        private set
        {
            m_playerScore = value;
            HUDManager.INSTANCE.SetScore(m_playerScore);
        }
    }

    public bool IsGameRunning
    {
        get
        {
            return m_isGameRunning;
        }
    }

    public bool IsGameWon
    {
        get
        {
            return m_isGameWon;
        }

        set
        {
            m_isGameWon = value;
            EndGame();
        }
    }

    void Start()
    {
    }

    public void StartGame()
    {
        PlayerScore = 0;
        m_isGameWon = false;
        MenuManager.INSTANCE.CloseMenu();
        EnemyManager.INSTANCE.StartSpawn();
        PlayerController.INSTANCE.StartGame();
        m_isGameRunning = true;
        SoundManager.INSTANCE.StartAudio(m_backGroundMusic, MIXER_GROUP_TYPE.AMBIANT, true, true, AUDIOSOURCE_KEY.BACKGROUND);
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
    }

    public void EndGame()
    {
        PlayerController.INSTANCE.CleanBoost();
        GameObjectManager.INSTANCE.DestroyObjects(SPAWN_CONTAINER_TYPE.DESTRUCTIBLE);
        EnemyManager.INSTANCE.StopSpawn();
        MenuManager.INSTANCE.OpenMenu(MENUTYPE.END);
        m_isGameRunning = false;
        Screen.sleepTimeout = SleepTimeout.SystemSetting;
    }

    void Update()
    {
    }

    internal void AddPoint(int a_enemyLevel)
    {
        PlayerScore += a_enemyLevel;
    }
}
