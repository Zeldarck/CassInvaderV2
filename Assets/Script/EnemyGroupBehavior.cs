using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class EnemyGroupBehavior : Ennemies
{
    [SerializeField]
    Ennemies m_EnemyPrefab;

    protected int enemyCount;
    protected int enemyStartCount;

    /// <summary>
    /// Later use to count the number of ennemies in the group
    /// </summary>
    override protected void Awake()
    {
        enemyStartCount = 1;
    }

    /// <summary>
    /// Add the enemy child in the group and link its transform to the group
    /// </summary>
    public void AddChild(Ennemies child)
    {
        child.transform.SetParent(transform);
    }

    /// <summary>
    /// In this override, we count the number of ennemies in the group once
    /// </summary>
    override protected void Start()
    {

        // IF enemyStartCount = 1 : 
        // enemyCount = this.transform.childCount; 
        // else : nothing

    }

    // Compute the direction vector of the enemy depending of the current situation
    override protected Vector2 DirectionComputation()
    {
        // Check if the invader is too close to the edge and change its direction accordingly
        if (gameObject.transform.position.x >= (_INIT_POS_X + _X_MOVE_EAST) || gameObject.transform.position.x <= (_INIT_POS_X + _X_MOVE_WEST))
        {
            _XDir *= -1;
        }
        // Use the number of ennmies in the wave to move everybody accordingly

        Vector2 directionComputed = new Vector2(_XDir, _YDir);
        directionComputed *= _enemySpeed;

        // Reset the _YDir variable to avoid the enemmies to keep going downward
        _YDir = 0;

        return directionComputed;
    }

    // Update the position of the enemy group
    override protected void FixedUpdate()
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
        gameObject.transform.position = (Vector2)gameObject.transform.position + direction * Time.deltaTime;

    }

    void Update()
    {
       
    }


}
