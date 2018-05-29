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
    }

    public void EndGame()
    {
        EnemyManager.INSTANCE.StopSpawn();
        GameObjectManager.INSTANCE.DestroyObjects(SPAWN_CONTAINER_TYPE.DESTRUCTIBLE);
        // MenuManager.INSTANCE.OpenMenu(a_type); //Open EndGame Menu, TODO
    }

    void Update()
    {
    }

    internal void AddPoint(int a_enemyLevel)
    {
        PlayerScore += a_enemyLevel;
    }
}
