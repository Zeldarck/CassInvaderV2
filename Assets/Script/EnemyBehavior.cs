using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class EnemyBehavior : Ennemies
{

    protected float m_enemyRadius = 1;
    protected int m_life = 1;

    public UnityEvent OnDie;


    #region GameObject Setup

    /// <summary>
    /// Start an OnDie Listener which whill trigger when an enemy shall be destroyed
    /// </summary>
    override protected void Start()
    {
        _INIT_POS_X = this.transform.position.x;
        _INIT_POS_Y = this.transform.position.y;

        _X_MOVE_EAST = (this.transform.parent.childCount)/10;
        _X_MOVE_WEST = (this.transform.parent.childCount)/10;

        OnDie.AddListener(() => { CollectableManager.INSTANCE.EnemyDestroyed(_enemyLevel, gameObject.transform.position); });
        OnDie.AddListener(() => StartCoroutine(AutoDestroy()));
    }

    #endregion

    #region Damage undertaking

    /// <summary>
    /// On trigger get damages from any damage source
    /// </summary>
    public bool GetDamage(int a_damage)
    {
        m_life -= a_damage;
        if (m_life <= 0)
        {
            OnDie.Invoke();
            return true;
        }
        return false;
    }

    /// <summary>
    /// Self Destruction on trigger and wait two frames in order to let the ball bounce on the collider
    /// Without this part of code, the ball would go through the enemy and not bounce
    /// </summary>
    protected IEnumerator AutoDestroy()
    {
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();
        Destroy(gameObject);
    }

    #endregion

    #region Movement behavior

    /// <summary>
    /// Compute the direction vector of the enemy depending of the current situation
    /// Here a default null vector is computed as this function shall be overriden by each kind of enemy
    /// </summary>
    override protected Vector2 DirectionComputation()
    {
        return new Vector2(0, 0);
    }

    /// <summary>
    /// Update the position of the enemy
    /// </summary>
    override protected void FixedUpdate()
    {
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
