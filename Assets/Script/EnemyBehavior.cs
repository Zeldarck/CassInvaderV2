using UnityEngine;

public class EnemyBehavior : Ennemies
{
 

    // Compute the direction vector of the enemy depending of the current situation
    override protected Vector2 DirectionComputation()
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

    // Update the position of the enemy
    override protected void FixedUpdate()
    {
        /*
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
        gameObject.transform.position = (Vector2)gameObject.transform.position + direction * Time.deltaTime;
        */
    }
}
