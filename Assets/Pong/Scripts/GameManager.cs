using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public enum DIFFICULTY { EASY, MEDIUM, HARD};
public enum PLAYER { NOBODY, PLAYER_1,PLAYER_2};

public class GameManager : MonoBehaviour {

    static GameManager INSTANCE;
    /// <summary>
    /// Score of player 1
    /// </summary>
    public static int m_playerScore1 = 0;
    /// <summary>
    /// Score of player 2
    /// </summary>
    public static int m_playerScore2 = 0;
    public static UnityEvent m_paused = new UnityEvent();
    public static UnityEvent m_unpaused = new UnityEvent();
    /// <summary>
    /// True if game paused
    /// </summary>
    public static bool m_isPaused = true;
    /// <summary>
    /// Score to win
    /// </summary>
    public static int m_scoreMax = 3;
    /// <summary>
    /// True if against AI
    /// </summary>
    public static bool m_isVsIA = false;
    /// <summary>
    /// True if objects spawn
    /// </summary>
    public static bool m_withObject = true;

    /// <summary>
    /// Who touch the ball last
    /// </summary>
    public static PLAYER m_lastHit = PLAYER.NOBODY;
    GameObject m_theBall;
    /// <summary>
    /// Who get a point last
    /// </summary>
    public static PLAYER m_lastWinner = PLAYER.NOBODY;
    /// <summary>
    /// Level of difficulty
    /// </summary>
    public static DIFFICULTY m_difficulty = DIFFICULTY.HARD;
    /// <summary>
    /// List of Players
    /// </summary>
    [SerializeField]
    List<PlayerControl> m_players = new List<PlayerControl>();
    /// <summary>
    /// Box wich spawn objects
    /// </summary>
    [SerializeField]
    GameObject m_boxPrefab;

    /// <summary>
    /// Actual time
    /// </summary>
    float m_timeSpawnBox = 0;
    /// <summary>
    /// Time between spawn
    /// </summary>
    [SerializeField]
    float m_IntervalSpawnBox;

    void Start()
    {
        if (INSTANCE != null && INSTANCE != this)
        {
            Destroy(gameObject);
        }
        else
        {
            INSTANCE = this;
            m_theBall = GameObject.FindGameObjectWithTag("Ball");
            m_paused.AddListener(Pause);
            m_unpaused.AddListener(UnPause);
        }
    }

    void Update()
    {
        if (!m_isPaused)
        {
            // If someone win
            if (m_playerScore1 == m_scoreMax || m_playerScore2 == m_scoreMax)
            {
                m_playerScore1 = 0;
                m_playerScore2 = 0;
                INSTANCE.m_theBall.SendMessage("ResetBall", null, SendMessageOptions.RequireReceiver);
                MenuManager.OpenMenu(MENUTYPE.END);
            }

            //Spawn a box
            m_timeSpawnBox += Time.deltaTime;
            if (m_withObject && m_timeSpawnBox > m_IntervalSpawnBox)
            {
                m_timeSpawnBox = 0;
                GameObject.Instantiate(m_boxPrefab, new Vector3(Random.Range(-1.4f, 1.4f), 0, 0), Quaternion.identity, transform);
            }
        }
    }

    void Pause()
    {
        m_isPaused = true;
        MenuManager.OpenMenu(MENUTYPE.PAUSE);
    }
    void UnPause()
    {
        m_isPaused = false;
        MenuManager.OpenMenu(MENUTYPE.GAME);
    }

    /// <summary>
    /// Began a game
    /// </summary>
    /// <param name="a_vsIA">True if against AI</param>
    public static void StartGame(bool a_vsIA)
    {
        INSTANCE.m_timeSpawnBox = 0;
        m_isVsIA = a_vsIA;
        m_lastWinner = PLAYER.NOBODY;
        m_lastHit = PLAYER.NOBODY;
        m_playerScore1 = 0;
        m_playerScore2 = 0;
        m_isPaused = false;
        foreach (PlayerControl player in INSTANCE.m_players)
        {
            player.RestartGame();
        }

        MenuManager.OpenMenu(MENUTYPE.GAME);


        RestartRoundStatic();
    }

    public static void RestartGame()
    {
        StartGame(m_isVsIA);
    }


    public static void RestartRoundStatic()
    {

        INSTANCE.m_timeSpawnBox = -1;
        for (int i = INSTANCE.transform.childCount - 1; i >= 0; i--)
        {
            Destroy(INSTANCE.transform.GetChild(i).gameObject);
        }


        if (m_playerScore1 != m_scoreMax && m_playerScore2 != m_scoreMax)
        {
            foreach (PlayerControl player in INSTANCE.m_players)
            {
                player.RestartGame();
            }

            INSTANCE.m_theBall.SendMessage("ResetBall", null, SendMessageOptions.RequireReceiver);
            INSTANCE.m_theBall.SendMessage("GoBall", 1, SendMessageOptions.RequireReceiver);
        }

    }

    /// <summary>
    /// Update score
    /// </summary>
    /// <param name="a_player">Wich player mark point</param>
    public static void Score(PLAYER a_player)
    {
        if (a_player == PLAYER.PLAYER_1)
        {
            m_playerScore1++;
            m_lastWinner = a_player;
        }
        else
        {
            m_lastWinner = PLAYER.PLAYER_2;
            m_playerScore2++;
        }
    }

    public static GameObject GetBallInfo()
    {
        return INSTANCE.m_theBall;
    }

    public static float NextGaussianDouble(float a_mean, float a_stdDev)
    {
        float u, v, S;

        do
        {
            u = 2.0f * Random.value - 1.0f;
            v = 2.0f * Random.value - 1.0f;
            S = u * u + v * v;
        } while (S >= 1.0f || S == 0f);

        float fac = Mathf.Sqrt((-2.0f * Mathf.Log(S) / S));
        return a_mean + a_stdDev * u * fac;
    }

    /// <summary>
    /// Boost ball speed
    /// </summary>
    /// <param name="a_value">Value of boost</param>
    public static void BoostBall(float a_value)
    {
        INSTANCE.m_theBall.SendMessage("Boost", a_value, SendMessageOptions.RequireReceiver);
    }





}
