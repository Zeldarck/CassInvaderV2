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
    [SerializeField]
    float m_boundY = 2.25f;
    private Rigidbody2D m_rb2d;

    /// <summary>
    /// To Delete
    /// </summary>
    public ButtonMove m_buttonRight;
    /// <summary>
    /// To Delete
    /// </summary>
    public ButtonMove m_buttonLeft;
    /// <summary>
    /// To Delete
    /// </summary>
    float t = 1000;

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

    void FixedUpdate() {

        t += Time.deltaTime;
        // if ((Input.GetButton("Fire1") || Input.GetKeyDown(KeyCode.Space) ) && t > 3.0f /* && m_chargeur.value == m_chargeur.maxValue*/)
        /* {
             Instantiate(m_ballPrefab, m_ballSpawnPosition.position, transform.rotation);
             t = 0;
             //TODO : add velocity depend on player velocity
         }*/


        var vel = m_rb2d.velocity;
        if (Input.touchCount > 0)
        {
            float x = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position).x;
            vel.x = m_speed * -1 * Utils.SignWithZero(transform.position.x - x);
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

  

   
    


}
