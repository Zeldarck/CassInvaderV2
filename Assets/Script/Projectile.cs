using UnityEngine;
using UnityEngine.Events;

public abstract class Projectile : MonoBehaviour
{

    protected float _INIT_POS_X = 0; // Initial X position of the projectile
    protected float _INIT_POS_Y = 0; // Initial Y position of the projectile

    #region GameObject Setup

    /// <summary>
    /// Set defaults values as soon as the object gets created
    /// </summary>
    protected virtual void Awake()
    {

    }

    /// <summary>
    /// General starting function for projectiles behavior
    /// </summary>
    protected virtual void Start()
    {
        _INIT_POS_X = this.transform.position.x;
        _INIT_POS_Y = this.transform.position.y;
    }

    #endregion

    #region GameObject movement Behavior

    /// <summary>
    /// Abstract function to compute the direction vector of the projectile depending of the current situation
    /// </summary>
    protected abstract Vector2 DirectionComputation();

    /// <summary>
    /// Abstract function to update the position of the projectile
    /// </summary>
    protected abstract void FixedUpdate();

    #endregion
}
