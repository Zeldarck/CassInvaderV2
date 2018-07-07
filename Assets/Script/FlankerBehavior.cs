using UnityEngine;

public class FlankerBehavior : EnemyBehavior
{

    private float m_rngHideStartLowLimit;
    private float m_rngHideStopLowLimit;
    private bool m_hidden;


    #region GameObject Setup

    /// <summary>
    /// Set default values proper to the kind of enemy created
    /// </summary>
    override protected void Awake()
    {
        m_enemyRadius = 1;
        m_life = 1;
        m_enemyLevel = 3;
        m_rngHideStartLowLimit = 0;
        m_rngHideStopLowLimit = 0;
        m_hidden = false;
    }

    #endregion

    #region Movement behavior

    /// <summary>
    /// Compute the direction vector of the enemy depending of the current situation
    /// Here the walker follow the group path slowly
    /// </summary>
    override protected Vector2 DirectionComputation()
    {
        return new Vector2(0,0);
    }

    override public bool GetDamage(int a_damage)
    {

        if (m_hidden)
        {
            a_damage = 0;
        }

        gameObject.GetComponent<Renderer>().enabled = false;
        m_hidden = true;
        m_rngHideStartLowLimit = 0;
        return base.GetDamage(a_damage);

    }

    /// <summary>
    /// Update the position of the enemy
    /// </summary>
    override protected void FixedUpdate()
    {
        // check if the invader is hidden
        if (m_hidden)
        {
            m_rngHideStopLowLimit += 0.01f;
            if (Random.Range(m_rngHideStopLowLimit, 1000) > 900)
            {
                gameObject.GetComponent<Renderer>().enabled = true;
                m_hidden = false;
                m_rngHideStopLowLimit = 0;
            }
        }

        else
        {
            m_rngHideStartLowLimit += 0.01f;
            if (Random.Range(m_rngHideStartLowLimit, 1000) > 900)
            {
                gameObject.GetComponent<Renderer>().enabled = false;
                m_hidden = true;
                m_rngHideStartLowLimit = 0;
            }
        }

        // check if an invader has made it to the Dead Zone
        if (gameObject.transform.position.y < (_DEADZONE + m_enemyRadius))
        {
            // GameController.EndGame(); // TO LINK OR IMPLEMENT LATER
            Debug.Log("An Enemy reached the bottom!");

            Destroy(gameObject);
        }

        // move accordingly 
        Vector2 direction = DirectionComputation();
        gameObject.transform.position = (Vector2)gameObject.transform.position + direction * Time.deltaTime;
    }

    #endregion

}
