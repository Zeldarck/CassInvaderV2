﻿using UnityEngine;

public class SweeperBehavior : EnemyBehavior
{
    [SerializeField]
    GameObject m_fireProjectile;

    private Vector3 m_currentPosition = new Vector3(0, 0, 0);
    private Quaternion m_initialRotation = new Quaternion(180, 0, 0, 0);

    #region GameObject Setup

    /// <summary>
    /// Set default values proper to the kind of enemy created
    /// </summary>
    override protected void Awake()
    {
        m_enemyRadius = 1;
        m_life = 2;
        m_enemyLevel = 2;

        InvokeRepeating("LaunchProjectile", Random.Range(0.8f, 12.1f), Random.Range(5f, 15f));
    }

    override public bool GetDamage(int a_damage)
    {
        return base.GetDamage(a_damage);
    }

    #endregion

    #region Movement behavior

    /// <summary>
    /// Compute the direction vector of the enemy depending of the current situation
    /// </summary>
    override protected Vector2 DirectionComputation()
    {
        return new Vector2(0, 0);
    }

    protected override void LaunchProjectile()
    {
        m_currentPosition = new Vector3(this.transform.position.x, this.transform.position.y, 0);
        GameObjectManager.INSTANCE.Instantiate(m_fireProjectile, m_currentPosition, m_initialRotation,SPAWN_CONTAINER_TYPE.DESTRUCTIBLE);
        base.LaunchProjectile();

    }

    #endregion

}
