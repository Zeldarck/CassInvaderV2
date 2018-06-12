using UnityEngine;
using UnityEngine.Events;

public abstract class Ennemies : MonoBehaviour {


    protected static float _DEADZONE = -4.1f; // Lower limit for which the game ends if an invader goes through

    protected float _X_MOVE_EAST = 1; // East movement limit per enemy
    protected float _X_MOVE_WEST = -1; // West movement limit per enemy
    protected float m_enemySpeed = 3;

    protected float _INIT_POS_X = 0; // Initial X position of the enemy
    protected float _INIT_POS_Y = 0; // Initial Y position of the enemy
    protected float _maxWindowX = 6.2f;
    protected float _XDir = 1;
    protected float _YDir = 0;


    #region GameObject Setup
    
    /// <summary>
    /// Set defaults values as soon as the object gets created
    /// </summary>
    protected virtual void Awake()
    {

    }
    
    /// <summary>
    /// General starting function for ennemies behavior
    /// </summary>
    protected virtual void Start()
    {
        _INIT_POS_X = this.transform.position.x;
        _INIT_POS_Y = this.transform.position.y;
    }

    #endregion

    #region GameObject movement Behavior

    /// <summary>
    /// Abstrat function to compute the direction vector of the ennemies depending of the current situation
    /// </summary>
    protected abstract Vector2 DirectionComputation();

    /// <summary>
    /// Abstrat function to update the position of ennemies
    /// </summary>
    protected abstract void FixedUpdate();

    #endregion
}
