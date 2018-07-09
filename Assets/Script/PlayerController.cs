using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerController : Singleton<PlayerController> {

    public KeyCode m_moveRight = KeyCode.D;
    public KeyCode m_moveLeft = KeyCode.Q;
    public float m_speed = 10.0f;
    protected float m_baseSpeed = 10.0f;
    public int m_countSlowDebuff = 0;
    protected int m_life = 10;
  
    /// <summary>
    /// Limit of move (considering center on x=0)
    /// </summary>
    [SerializeField]
    float m_boundY = 2.25f;
    private Rigidbody2D m_rb2d;


    //mettr eun temps d ereload et un nombre de ball limité 


    /// <summary>
    /// Time to reload
    /// </summary>
    [SerializeField]
    float m_speedOfReload = 10;

    float m_currentSpeedOfReload;


    /// <summary>
    /// Nb Bamm max at one time
    /// </summary>
    [SerializeField]
    int m_nbMaxBall = 1;


    /// <summary>
    /// Slider UI
    /// </summary>
    [SerializeField]
    Slider m_sliderReload;


    /// <summary>
    /// Ball Prefab
    /// </summary>
    [SerializeField]
    GameObject m_ballPrefab;

    /// <summary>
    /// SpawnBall Position
    /// </summary>
    [SerializeField]
    Transform m_ballSpawnPosition;

    private Vector3 m_startPos;

    /// <summary>
    /// PercentageScreenFire
    /// </summary>
    [SerializeField]
    float m_percentageScreenFire;

    [SerializeField]
    float m_multiplicatorIfNoBall;

    float m_xLastinput = 0;
    float m_dir = 0;
    float m_currentSpeed = 0;
    bool m_haveLastInput = false;
    [SerializeField]
    float m_stepSpeed;

    [SerializeField]
    int m_controlType;

    /// <summary>
    /// Collectable boosts
    /// </summary>
    private Collectable m_boostCollected = null;
    [SerializeField]
    Image m_boostHUD = null;
    private bool m_boostPicked = false;
    private bool m_boostUsable = false;

    Color m_boostHUDDestColor = Color.black;
    float m_timeLerpBoostHUD = 0;

    void Start()
    {
        m_rb2d = GetComponent<Rigidbody2D>();
        m_startPos = transform.position;
        m_sliderReload.maxValue = 100;// m_timeToReload;
        m_sliderReload.value = m_sliderReload.maxValue;
        m_baseSpeed = m_speed;
        m_sliderReload.onValueChanged.AddListener((float a_value) =>
       {
           if(a_value >= m_sliderReload.maxValue)
           {
               SoundManager.INSTANCE.StartAudio(AUDIOCLIP_KEY.RELOADED, MIXER_GROUP_TYPE.SFX_GOOD, false, false, AUDIOSOURCE_KEY.NO_KEY_AUTODESTROY);
           }
       });
    }

    private void Update()
    {
        var pos = transform.position;
        pos.x = Mathf.Clamp(pos.x, -m_boundY, m_boundY);
        transform.position = pos;
        if(m_boostCollected != null)
        {
            m_timeLerpBoostHUD = Math.Min(m_timeLerpBoostHUD + Time.deltaTime, m_boostCollected.BoostDuration);
            m_boostHUD.color = Color.Lerp(m_boostCollected.GetColorPower(), m_boostHUDDestColor, m_timeLerpBoostHUD/m_boostCollected.BoostDuration);
        }
        else
        {
            m_boostHUD.color = Color.black;
        }

    }

    void FixedUpdate()
    {
        if (!GameManager.INSTANCE.IsGameRunning)
        {
            return;
        }

        m_sliderReload.value += (Time.deltaTime * ( BallController.NbBallAlive > 0 ? 1 : m_multiplicatorIfNoBall) * m_currentSpeedOfReload);
        if ( Input.GetKeyDown(KeyCode.Space) && m_sliderReload.value >= m_sliderReload.maxValue && BallController.NbBallAlive < m_nbMaxBall)
        {
            Fire(new Vector2(25, 15));
        }

        
        if(m_countSlowDebuff != 0)
        {
            --m_countSlowDebuff;
            if (m_countSlowDebuff == 0)
            {
                m_speed = m_baseSpeed;
            }
        }

        if (Input.GetKeyDown(KeyCode.E) && m_boostUsable)
        {
            UseBoost();
        }
        
        var vel = m_rb2d.velocity;
        if (Input.touchCount > 0)
        {

            if (Utils.IsTapping(Input.GetTouch(0), 0) && Input.GetTouch(0).position.y > Screen.height * m_percentageScreenFire && m_sliderReload.value >= m_sliderReload.maxValue && BallController.NbBallAlive < m_nbMaxBall)
            {
                Debug.Log(Input.GetTouch(0).position.y + "   " + Screen.height * m_percentageScreenFire);
                Fire((Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position) - m_ballSpawnPosition.position).normalized);
            }
            else if (m_boostUsable && Utils.IsTapping(Input.GetTouch(0)) && Input.GetTouch(0).position.y <= Screen.height * m_percentageScreenFire)
            {
                UseBoost();
            }
            else if (Input.GetTouch(0).position.y <= Screen.height * m_percentageScreenFire)
            {
                float x = Input.GetTouch(0).position.x;
                float pos = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position).x;

                if ((m_controlType == 0 && m_haveLastInput && x < m_xLastinput + Mathf.Epsilon) || (m_controlType == 1 && pos < transform.position.x - 0.1f) || (m_controlType == 2 && pos < 0))
                {
                    m_currentSpeed = (m_dir > 0 ? m_stepSpeed : m_currentSpeed + m_stepSpeed);
                    m_dir = -1;
                }
                else if ((m_controlType == 0 && m_haveLastInput && x > m_xLastinput - Mathf.Epsilon) || (m_controlType == 1 && pos > transform.position.x + 0.1f) || (m_controlType == 2 && pos > 0))
                {
                    m_currentSpeed = (m_dir < 0 ? m_stepSpeed : m_currentSpeed + m_stepSpeed);
                    m_dir = 1;
                }
                else if ((!Utils.IsSameSign(pos - transform.position.x, m_dir) && m_controlType == 0) || (m_controlType!=0))
                {
                    m_dir = 0;
                    m_currentSpeed = 0;
                }
                else
                {
                    m_currentSpeed += m_stepSpeed;
                }
                vel.x = m_speed * Mathf.Log10(m_currentSpeed + m_speed + 10) * m_dir;
                m_haveLastInput = true;
                m_xLastinput = x;


            }



        }
        else {
            m_dir = 0;
            m_currentSpeed = 0;
            m_haveLastInput = false;

            if (Input.GetKey(m_moveRight) || Input.GetKey(KeyCode.RightArrow))
            {
                vel.x = m_speed;
            }
            else if (Input.GetKey(m_moveLeft) || Input.GetKey(KeyCode.LeftArrow))
            {
                vel.x = -m_speed;
            }
            else
            {
                vel.x = 0;
            }
        }


        m_rb2d.velocity = vel;

    }

    void Fire(Vector2 a_direction)
    {
        GameObject ball = GameObjectManager.INSTANCE.Instantiate(m_ballPrefab, m_ballSpawnPosition.position, transform.rotation, SPAWN_CONTAINER_TYPE.DESTRUCTIBLE);
        ball.GetComponent<BallController>().LaunchBall(0, a_direction);
        m_sliderReload.value = 0;
    }

    public void StartGame()
    {
        transform.position = new Vector3(0, transform.position.y, transform.position.z);
        m_sliderReload.value = m_sliderReload.maxValue;
        m_currentSpeedOfReload = m_speedOfReload;
        m_currentSpeed = m_speed;
    }


    /// <summary>
    /// Reset the paddle
    /// </summary>
    public void ResetPosition()
    {
        transform.position = m_startPos;
    }

    public void SetReloadSpeed(float a_reloadSpeed)
    {
        m_currentSpeedOfReload = a_reloadSpeed;
    }

    public void ResetReloadSpeed()
    {
        m_currentSpeedOfReload = m_speedOfReload;
    }

    public void SetNbMaxBall(int a_nbBall)
    {
        m_nbMaxBall = a_nbBall;
    }

    public void AddNbMaxBall(int a_nbBall)
    {
        m_nbMaxBall += a_nbBall;
        if (m_nbMaxBall < 1) m_nbMaxBall = 1;
    }

    public void UseBoost()
    {
        if (m_boostCollected != null)
        {
            Debug.Log("BoostUsed");
            SetUsableBoost(false);
            m_boostCollected.PlayerUse();
            m_boostHUDDestColor = Color.black;
            m_timeLerpBoostHUD = 0;
        }
    }

    public void DestroyBoost()
    {
        if (m_boostUsable)
        {
            CleanBoost();
        }
    }

    public void CleanBoost()
    {
        SetActiveBoost(false);
        SetBoost(null);
    }


    public void SetBoost(Collectable a_boost)
    {
        Debug.Log("SetBoost");
        m_boostCollected = a_boost;
        if(a_boost != null)
        {
            m_boostHUDDestColor = a_boost.GetColorPower();
            SetUsableBoost(true);
            SetActiveBoost(true);
        }
        
    }

    public void SetActiveBoost(bool a_isActive)
    {
        m_boostPicked = a_isActive;
    }

    public void SetUsableBoost(bool a_isUsable)
    {
        m_boostUsable = a_isUsable;
    }

    public bool IsBoostActive()
    {
        return m_boostPicked;
    }

    public bool GetDamage(int a_damage)
    {
        m_life -= a_damage;
        if (m_life <= 0)
        {
            Debug.Log("Vous avez perdu, vous etes nuls, noob go uninstall please !!");
            return true;
        }

        return false;
    }

}
