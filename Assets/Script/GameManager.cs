using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;
using UnityEngine.UI;


public class GameManager : MonoBehaviour {

    public static GameManager INSTANCE;
    /// <summary>
    /// Score of player 1
    /// </summary>
    static int m_playerScore = 0;


    public static int PlayerScore
    {
        get
        {
            return m_playerScore;
        }
    }

    void Start()
    {
        if (INSTANCE != null && INSTANCE != this)
        {
            Destroy(gameObject);
        }
        else
        {
            INSTANCE = this;
        }
    }

    public void StartGame()
    {
        m_playerScore = 0;
        MenuManager.INSTANCE.CloseMenu();
        EnemyManager.INSTANCE.StartSpawn();
    }

    void Update()
    {
    }


}
