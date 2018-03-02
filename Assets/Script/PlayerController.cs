using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerController : MonoBehaviour {

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
        m_rb2d.velocity = vel;

    }


    /// <summary>
    /// Reset the paddle
    /// </summary>
    public void RestartGame()
    {
      
        transform.position = m_startPos;

    }

  

   
    


}
