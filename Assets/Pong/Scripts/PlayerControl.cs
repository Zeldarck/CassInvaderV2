using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerControl : MonoBehaviour {

    public KeyCode m_moveRight = KeyCode.D;
    public KeyCode m_moveLeft = KeyCode.Q;
    public float m_speed = 10.0f;

    /// <summary>
    /// Limit of move (considering center on x=0)
    /// </summary>
    public float m_boundY = 2.25f;
    private Rigidbody2D m_rb2d;

    public ButtonMove m_buttonRight;
    public ButtonMove m_buttonLeft;

    private Vector3 m_startPos;

    /// <summary>
    /// True if is an AI
    /// </summary>
    bool m_isAI = false;

    /// <summary>
    /// True if this paddle can be an AI
    /// </summary>
    public bool m_canBeAI = false;
    public PLAYER m_playerID;
    /// <summary>
    /// Currently got Pickup
    /// </summary>
    PickUp m_pickUp =null;


    void Start () {
        m_rb2d = GetComponent<Rigidbody2D>();
        m_startPos = transform.position;
    }

    private void Update()
    {
        var pos = transform.position;
        pos.x = Mathf.Clamp(pos.x, -m_boundY, m_boundY);
        transform.position = pos;

    }

    void FixedUpdate () {
        var vel = m_rb2d.velocity;
        if (!m_isAI)
        {
            if (Input.GetKey(m_moveRight) || m_buttonRight.m_isPointerIn)
            {
                vel.x = m_speed;
            }
            else if (Input.GetKey(m_moveLeft) ||m_buttonLeft.m_isPointerIn)
            {
                vel.x = -m_speed;
            }
            else
            {
                vel.x = 0;
            }
        }
        else
        {
            vel.x = GetVelocityIA();
            UsePickUpIA();

        }
        m_rb2d.velocity = vel;


    }


    /// <summary>
    /// Reset the paddle
    /// </summary>
    public void RestartGame()
    {
         m_isAI = m_canBeAI ? GameManager.m_isVsIA : false;
        transform.position = m_startPos;
        m_buttonRight.transform.parent.gameObject.SetActive(!m_isAI);

    }

    /// <summary>
    /// Return the x velocity of AI
    /// </summary>
    public float GetVelocityIA()
    {
        float res = 0;
        GameObject ball = GameManager.GetBallInfo();
        Vector2 ballPos = ball.transform.position;
        Vector2 pos = transform.position;

        float diff = ballPos.x - pos.x;

        float handicap = GetAIHandicap();
        //If in line of sight and not near in x
        if (Mathf.Abs(diff) < 0.30f  || Mathf.Abs(pos.y - ballPos.y) > GameManager.NextGaussianDouble(3f-handicap, 0.2f+ handicap))
        {
            res = 0;
        }
        else 
        {
            diff = diff > 0 ? 1 : -1;
            res = diff * GameManager.NextGaussianDouble(m_speed- handicap, 0.25f+ handicap) ;
        }
        return res;
    }

    /// <summary>
    /// Return a value to set difficulty of AI
    /// </summary>
    float GetAIHandicap()
    {
        float res = 0;
        switch (GameManager.m_difficulty)
        {
            case DIFFICULTY.EASY:
                res = 0.65f;
                break;
            case DIFFICULTY.MEDIUM:
                res = 0.45f;
                break;
            case DIFFICULTY.HARD:
                res = 0;
                break;

        }
        return res;
    }

    /// <summary>
    /// Determine if AI use is PickUp
    /// </summary>
    public void UsePickUpIA()
    {
        if (m_pickUp)
        {
            GameObject ball = GameManager.GetBallInfo();
            float direction = ball.GetComponent<Rigidbody2D>().velocity.y;
            PICKUPTYPE acceptType = direction / transform.position.y > 0 ? PICKUPTYPE.LOW : PICKUPTYPE.BOOST;


            Vector2 ballPos = ball.transform.position;
            Vector2 pos = transform.position;


            if (m_pickUp.GetPickUpType() == acceptType && InDistOfUseAI(acceptType, Mathf.Abs(pos.y - ballPos.y)))
            {
                UsePickUp();
            }
        }

    }

    /// <summary>
    /// Test distance to use pickup for AI
    /// </summary>
     bool InDistOfUseAI(PICKUPTYPE a_type,float a_dist)
    {
        bool res = true;
        switch (a_type)
        {
            case PICKUPTYPE.BOOST:
                res = a_dist > 1.5f;

            break;
            case PICKUPTYPE.LOW:
                res = a_dist < 2;
            break;

        }

            return res;
    }


    /// <summary>
    /// Add a pick up to the player
    /// </summary>
    /// <returns>if false the pick up haven't been set to the player</returns>
    public bool SetPickUp(PickUp a_pickUp)
    {

        if (m_pickUp && m_pickUp != a_pickUp)
        {
    
            return false;
        }

        m_pickUp = a_pickUp;
        return true;
    }

    /// <summary>
    /// Use the current pick Up
    /// </summary>
    public void UsePickUp()
    {
        if (m_pickUp)
        {
            m_pickUp.PlayerUse();
        }
    }

    /// <summary>
    /// Get color for power button
    /// </summary>
    public Color GetColorPower()
    {
        if (m_pickUp)
        {
           return m_pickUp.GetColorPower();
        }
        else
        {
            return  Color.black;
        }
    }



}
