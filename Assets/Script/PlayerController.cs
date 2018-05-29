﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerController : MonoBehaviour {

    public KeyCode m_moveRight = KeyCode.D;
    public KeyCode m_moveLeft = KeyCode.Q;
    public float m_speed = 10.0f;
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
    float m_timeToReload = 30;


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


    /// <summary>
    /// Collectable boosts
    /// </summary>
    [SerializeField]
    private Collectable m_boostCollected = null;
    [SerializeField]
    private Button m_boostButton = null;
    private bool m_boostPicked = false;
    private bool m_boostUsable = false;



    void Start()
    {
        m_rb2d = GetComponent<Rigidbody2D>();
        m_startPos = transform.position;
        m_sliderReload.maxValue = m_timeToReload;
        m_sliderReload.value = m_sliderReload.maxValue;
    }

    private void Update()
    {
        var pos = transform.position;
        pos.x = Mathf.Clamp(pos.x, -m_boundY, m_boundY);
        transform.position = pos;

    }

    void FixedUpdate()
    {

        m_sliderReload.value += Time.deltaTime;
        if ( Input.GetKeyDown(KeyCode.Space) && m_sliderReload.value >= m_sliderReload.maxValue && BallController.NbBallAlive < m_nbMaxBall)
        {
            Instantiate(m_ballPrefab, m_ballSpawnPosition.position, transform.rotation);
            m_sliderReload.value = 0;
            //TODO : add velocity depend on player velocity
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

                Instantiate(m_ballPrefab, m_ballSpawnPosition.position, transform.rotation);
                m_sliderReload.value = 0;
                //TODO : add velocity depend on player velocity
            }
            else if(m_boostUsable && Utils.IsTapping(Input.GetTouch(0)) && Input.GetTouch(0).position.y <= Screen.height * m_percentageScreenFire)
            {
                UseBoost();
            }
            else
            {
                float x = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position).x;
                vel.x = m_speed * -1 * Utils.SignWithZero(transform.position.x - x, 0.1f);
            }
        }
        else if (Input.GetKey(m_moveRight) || Input.GetKey(KeyCode.RightArrow))
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


        m_rb2d.velocity = vel;

    }


    /// <summary>
    /// Reset the paddle
    /// </summary>
    public void ResetPosition()
    {
        transform.position = m_startPos;
    }

    public void SetReloadTime(float a_reloadTime)
    {
        m_timeToReload = a_reloadTime;
        m_sliderReload.maxValue = m_timeToReload;
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
            m_boostButton.image.color = Color.white;
        }
    }


    public void SetBoost(Collectable a_boost)
    {
        Debug.Log("SetBoost");
        m_boostCollected = a_boost;
        if(a_boost != null)
        {
            m_boostButton.image.color = a_boost.GetColorPower();
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
