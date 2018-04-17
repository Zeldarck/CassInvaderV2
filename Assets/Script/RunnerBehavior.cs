using UnityEngine;

public class RunnerBehavior : EnemyBehavior
{

    #region GameObject Setup

    /// <summary>
    /// Set default values proper to the kind of enemy created
    /// </summary>
    override protected void Awake()
    {
        m_enemySpeed = 0.8f;
        m_enemyRadius = 1;
        m_life = 1;
        m_enemyLevel = 2;
    }

    #endregion

    #region Movement behavior

    /// <summary>
    /// Compute the direction vector of the enemy depending of the current situation
    /// </summary>
    override protected Vector2 DirectionComputation()
    {
        // Check if the invader is too close to the edge and change its direction accordingly
        if (m_frameCount >= 10)
        {
            _XDir *= -1;
            m_frameCount = 0;
        }

        Vector2 directionComputed = new Vector2(_XDir, 0);
        directionComputed *= m_enemySpeed;
        ++m_frameCount;

        return directionComputed;
    }

    #endregion

}
