using UnityEngine;


public class EnemyGroupBehavior : Ennemies
{
    [SerializeField]
    Ennemies m_EnemyPrefab;
        
    #region ChildsManagement

    /// <summary>
    /// Add the enemy child in the group and link its transform to the group
    /// </summary>
    public void AddChild(Ennemies child)
    {
        child.transform.SetParent(transform);
    }

    /// <summary>
    /// Check if there are still childs within the enemy group
    /// </summary>
    protected bool IsChilds()
    {
        if (this.transform.childCount <= 0)
        {
            return true;
        }

        else
        {
            return false;
        }
    }


    #endregion

    #region MovementBehavior

    /// <summary>
    /// Compute the direction vector of the enemy group depending of the current situation
    /// </summary>
    override protected Vector2 DirectionComputation()
    {
        // Check if the invader is too close to the edge and change its direction accordingly
        if (gameObject.transform.position.x >= (_INIT_POS_X + _X_MOVE_EAST) || gameObject.transform.position.x <= (_INIT_POS_X + _X_MOVE_WEST))
        {
            _XDir *= -1;
            _YDir = -25;
        }
        // Use the number of ennmies in the wave to move everybody accordingly

        Vector2 directionComputed = new Vector2(_XDir, _YDir);
        directionComputed *= m_enemySpeed;

        // Reset the _YDir variable to avoid the enemmies to keep going downward
        _YDir = 0;

        return directionComputed;
    }

    /// <summary>
    /// Update the position of the enemy group
    /// </summary>
    override protected void FixedUpdate()
    {
        // Destroy the enemygroup when it has no more enemmies within
        if (IsChilds())
        {
            Destroy(gameObject);
        }

        // move accordingly 
        Vector2 direction = DirectionComputation();
        gameObject.transform.position = (Vector2)gameObject.transform.position + direction * Time.deltaTime;
    }

    #endregion
}
