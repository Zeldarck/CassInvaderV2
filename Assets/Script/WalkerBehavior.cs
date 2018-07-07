using UnityEngine;

public class WalkerBehavior : EnemyBehavior
{


    #region GameObject Setup

    /// <summary>
    /// Set default values proper to the kind of enemy created
    /// </summary>
    override protected void Awake()
    {
        m_enemyRadius = 1;
        m_life = 1;
        m_enemyLevel = 1;
    }

    override public bool GetDamage(int a_damage)
    {
        return base.GetDamage(a_damage);
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

    #endregion

}
