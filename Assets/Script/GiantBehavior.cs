using UnityEngine;

public class GiantBehavior : EnemyBehavior
{


    #region GameObject Setup

    /// <summary>
    /// Set default values proper to the kind of enemy created
    /// </summary>
    override protected void Awake()
    {
        m_localMovementSpeed = 0.1f;
        m_enemyRadius = 2;
        m_life = 4;
        m_enemyLevel = 3;

    }

    #endregion

    override public bool GetDamage(int a_damage)
    {

        return base.GetDamage(a_damage);
    }

    #region Movement behavior

    /// <summary>
    /// Compute the direction vector of the enemy depending of the current situation
    /// The behavior of this enemy is : TBC
    /// </summary>
    override protected Vector2 DirectionComputation()
    {
        // Check if the invader is too close to the edge and change its direction accordingly
        if (gameObject.transform.position.x >= (_INIT_POS_X + _X_MOVE_EAST) || gameObject.transform.position.x <= (_INIT_POS_X + _X_MOVE_WEST))
        {
            _XDir *= -1;
        }

        Vector2 directionComputed = new Vector2(_XDir, 0);
        directionComputed *= m_localMovementSpeed;

        return directionComputed;
    }

    #endregion

}
