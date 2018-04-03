using System.Collections;
using UnityEngine;
using UnityEngine.Events;


public abstract class Ennemies : MonoBehaviour {


    protected static float _DEADZONE = -3.1f; // Lower limit for which the game ends if an invader goes through
    protected float _X_MOVE_EAST = 1; // East movement limit per enemy
    protected float _X_MOVE_WEST = -1; // West movement limit per enemy

    protected float _INIT_POS_X = 0; // Initial X position of the enemy
    protected float _INIT_POS_Y = 0; // Initial Y position of the enemy
    protected float _XDir = 1;
    protected float _YDir = 0;

    protected float _enemyRadius;
    protected float _enemySpeed = 0.5f;
    protected int m_life = 1;

    public UnityEvent OnDie;

    /// <summary>
    /// Set defaults values as soon as the object get created
    /// </summary>
    protected virtual void Awake()
    {
        // Set default values
        _enemySpeed = 1;
        _enemyRadius = 0.25f;
    }
    
    /// <summary>
    /// General starting function for ennemies behavior
    /// </summary>
    protected virtual void Start()
    {
        OnDie.AddListener(() => StartCoroutine(AutoDestroy()));
    }

    /// <summary>
    /// Function managing the apparition of ennemies groups as waves
    /// Inputs : • numberEnnemies : the number of ennemies in the group
    ///          • type : type of ennemies in the group
    /// </summary>
    protected void EnemyManager(int numberEnnemies, int type)
    {
        // TO DO in an another class
    }

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
    /// Self Destruction on trigger
    /// </summary>
    protected IEnumerator AutoDestroy()
    {
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();
        Destroy(gameObject);
    }

    /// <summary>
    /// Abstrat function to compute the direction vector of the ennemies depending of the current situation
    /// </summary>
    protected abstract Vector2 DirectionComputation();

    /// <summary>
    /// Abstrat function to update the position of ennemies
    /// </summary>
    protected abstract void FixedUpdate();

}
