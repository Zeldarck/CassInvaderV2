using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class EnemyBehavior : MonoBehaviour {

    protected static float _DEADZONE = -3.1f; // Lower limit for which the game ends if an invader goes through

    // Parameters below to be used in the Group manager !!
    protected static float _X_MOVE_EAST = 1; // East movement limit per enemy
    protected static float _X_MOVE_WEST = -1; // West movement limit per enemy
    protected static float _enemyRadius = 0.25f;

    // Not useful anymore
    protected float _INIT_POS_X = 0; // Initial X position of the enemy
    protected float _INIT_POS_Y = 0; // Initial Y position of the enemy

    protected float _XDir = 1;
    protected float _YDir = 0;

    [SerializeField]
    protected float _enemySpeed;

    [SerializeField]
    protected int m_life;

    public UnityEvent OnDie;

    // Use this for initialization
    protected void Start()
    {
        // Set the initial position of the enemy for later use
        _INIT_POS_X = gameObject.transform.position.x;
        _INIT_POS_Y = gameObject.transform.position.y;

        Debug.Log(_INIT_POS_X);
        Debug.Log(_INIT_POS_Y);

        OnDie.AddListener(() => StartCoroutine(AutoDestroy()));    
    }
    
    // On trigger get damages from any damage source
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

    // Self Destruction on trigger
    IEnumerator AutoDestroy()
    {
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();
        Destroy(gameObject);
    }
    
    // Compute the direction vector of the enemy depending of the current situation
    protected Vector2 DirectionComputation()
    {
        // Check if the invader is too close to the edge and change its direction accordingly
        if (gameObject.transform.position.x >= (_INIT_POS_X + _X_MOVE_EAST) || gameObject.transform.position.x <= (_INIT_POS_X + _X_MOVE_WEST))
        {
            _XDir *= -1;
            _YDir = -10;
        }

        Vector2 directionComputed = new Vector2(_XDir, _YDir);
        directionComputed *= _enemySpeed;

        // Reset the _YDir variable to avoid the enemmies to keep going downward
        _YDir = 0;

        return directionComputed;
    }


    // Position update which is called once per frame
    void FixedUpdate()
    {
        // check if an invader has made it to the Dead Zone
        if (gameObject.transform.position.y < (_DEADZONE + _enemyRadius))
        {
            Debug.Log("An Enemy reached the bottom!");

            // GameController.EndGame(); // TO LINK OR IMPLEMENT LATER
            Destroy(gameObject);
        }
                       
        // move accordingly 
        Vector2 direction = DirectionComputation();
        Debug.Log(direction);
        gameObject.transform.position = (Vector2) gameObject.transform.position + direction * Time.deltaTime;
    }
}
