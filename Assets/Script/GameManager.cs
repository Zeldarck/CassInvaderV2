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

    [SerializeField]
    AudioClip m_noemieTest;

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

    void Start()
    {
        if (m_noemieTest != null)
        {
            SoundManager.INSTANCE.StartAudio(m_noemieTest);
        }
    }

    public void StartGame()
    {
        PlayerScore = 0;
        MenuManager.INSTANCE.CloseMenu();
        EnemyManager.INSTANCE.StartSpawn();
        PlayerController.INSTANCE.StartGame();
        m_isGameRunning = true;
    }

    public void EndGame()
    {
        GameObjectManager.INSTANCE.DestroyObjects(SPAWN_CONTAINER_TYPE.DESTRUCTIBLE);
        EnemyManager.INSTANCE.StopSpawn();
        MenuManager.INSTANCE.OpenMenu(MENUTYPE.END);
        m_isGameRunning = false;
    }

    void Update()
    {
    }

    internal void AddPoint(int a_enemyLevel)
    {
        PlayerScore += a_enemyLevel;
    }
}
