using UnityEngine;

public class RunnerBehavior : EnemyBehavior
{

    [SerializeField]
    GameObject m_iceProjectile;

    private Vector3 m_currentPosition = new Vector3(0, 0, 0);
    private Quaternion m_initialRotation = new Quaternion(180, 0, 0, 0);

    #region GameObject Setup

    /// <summary>
    /// Set default values proper to the kind of enemy created
    /// </summary>
    override protected void Awake()
    {
        m_enemySpeed = 0.65f;
        m_enemyRadius = 1;
        m_life = 1;
        m_enemyLevel = 2;

        InvokeRepeating("LaunchProjectile", Random.Range(0.8f, 12.1f), Random.Range(5f, 15f));
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

    void LaunchProjectile()
    {
        m_currentPosition = new Vector3(this.transform.position.x, this.transform.position.y, 0);
        Instantiate(m_iceProjectile, m_currentPosition, m_initialRotation);
    }

    #endregion

}
