using System.Collections;
using UnityEngine;

public class BallController : MonoBehaviour {

    static int m_nbBallAlive = 0;

    private Rigidbody2D m_rb2d;
    /// <summary>
    /// Velocity if pause
    /// </summary>
    private Vector2 m_vel;

    /// <summary>
    /// Basic speed
    /// </summary>
    [SerializeField]
    float m_speed;

    /// <summary>
    /// Speed during game
    /// </summary>
    float m_currentSpeed;

    /// <summary>
    /// how evolve speed
    /// </summary>
    [SerializeField]
    float m_step = 0.05f;


    /// <summary>
    /// damage done
    /// </summary>
    [SerializeField]
    int m_strength = 1;

    /// <summary>
    /// Minimum horizontal Angle in degree
    /// </summary>
    [SerializeField]
    float m_minAngle = 20;


    /// <summary>
    /// Modificator of speed
    /// </summary>
    float m_countBoost = 0;


    public static int NbBallAlive
    {
        get
        {
            return m_nbBallAlive;
        }
    }

    void Start()
    {
        m_rb2d = GetComponent<Rigidbody2D>();
        m_currentSpeed = m_speed;
        ++m_nbBallAlive;
    }

    public void LaunchBall(float a_delay, Vector2 a_direction)
    {
        GoBall(a_delay, a_direction);
    }

    void Update () {
        m_rb2d.velocity = m_rb2d.velocity.normalized * m_currentSpeed;
    }

    void Paused() {
       m_vel = m_rb2d.velocity;     
       m_rb2d.velocity = Vector2.zero;
    }

    void UnPaused()
    {
        m_rb2d.velocity = m_vel;
    }


    /// <summary>
    /// The ball started
    /// </summary>
    void GoBall(float a_second, Vector2 a_direction)
    {
        StartCoroutine(GoBallCoroutine(a_second, a_direction));
    }

    IEnumerator GoBallCoroutine(float a_second, Vector2 a_direction)
    {
        yield return new WaitForSeconds(a_second);

        GetComponent<TrailRenderer>().enabled = true;
        m_rb2d.AddForce(a_direction);
        
    }

    void ResetBall()
    {
        GetComponent<TrailRenderer>().enabled = false;
        m_rb2d.velocity = Vector2.zero;
        transform.position = Vector2.zero;
        m_currentSpeed = m_speed;
        m_countBoost = 0;
        UpdateColorBall();
        StopAllCoroutines();

    }



    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.collider.CompareTag("Player"))
        {
            UpdateColorBall();

            Vector2 vel = Vector2.zero;
            if (coll.contacts.Length >= 1)
            {
                vel.x = m_currentSpeed * (coll.contacts[0].point.x - coll.collider.attachedRigidbody.position.x);
            }
            //Force y velocity to avoid ball move horizontally
            if (m_rb2d)
            {
                m_currentSpeed += m_step;
                vel.y = m_rb2d.velocity.y;
                vel = Utils.ClampVector(vel, m_minAngle, 360 - m_minAngle);
                vel = Utils.ClampVector(vel, 180 + m_minAngle, 180 - m_minAngle) * m_currentSpeed;

                m_rb2d.velocity = vel;
            }


        }
        else if (coll.collider.CompareTag("Wall"))
        {
            Vector2 vel = Vector2.zero;
            //Force y velocity to avoid ball move horizontally
            if (m_rb2d)
            {
                vel = Utils.ClampVector(m_rb2d.velocity, m_minAngle, 360 - m_minAngle) ;
                vel = Utils.ClampVector(vel, 180 + m_minAngle, 180 - m_minAngle) * m_currentSpeed;
                m_rb2d.velocity = vel;
            }

        }
        EnemyBehavior ennemy =  coll.gameObject.GetComponent<EnemyBehavior>();

        if (ennemy)
        {
            ennemy.GetDamage(m_strength);
        }
    }


    /// <summary>
    /// Boost Speed
    /// </summary>
    public void BoostSpeed(float a_value, float a_time)
    {
        m_countBoost += a_value;
        m_currentSpeed += a_value;
        UpdateColorBall();
        StartCoroutine(UndoBoostSpeed(a_value, a_time));
    }


    IEnumerator UndoBoostSpeed(float a_value, float a_time)
    {
        yield return new WaitForSeconds(a_time);
        m_countBoost -= a_value;
        Debug.Log(m_countBoost);
        m_currentSpeed -= a_value;
        UpdateColorBall();
    }

    public void BoostStrength(int a_value, float a_time)
    {
        m_strength += a_value;
        StartCoroutine(UndoBoostStrength(a_value, a_time));
    }


    IEnumerator UndoBoostStrength(int a_value, float a_time)
    {
        yield return new WaitForSeconds(a_time);
        m_strength -= a_value;
    }

    void UpdateColorBall()
    {
        Color color = m_countBoost > 0 ? Color.red : m_countBoost == 0 ? Color.white : Color.blue;
        GetComponent<SpriteRenderer>().color = color;

        TrailRenderer trail = GetComponent<TrailRenderer>();
        color.a = trail.startColor.a;
        trail.startColor = color;
        color.a = trail.endColor.a;
        trail.endColor = color;

    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        --m_nbBallAlive;
    }


}
