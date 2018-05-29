using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class FireProjectileBehavior : Projectile
{
    protected float m_projectileSpeed = 3f;
    public UnityEvent OnDie;

    #region Damage undertaking


    private void OnTriggerEnter2D(Collider2D collision)
    {
        Collider2D collider = collision.GetComponent<Collider2D>();
        PlayerController player = collision.gameObject.GetComponent<PlayerController>();
        if (collider.CompareTag("Player"))
        {
            if (player.GetDamage(1))
            {
                GameObjectManager.INSTANCE.DestroyObjects(SPAWN_CONTAINER_TYPE.DESTRUCTIBLE);
            }

            player.SetUsableBoost(false);
            GetComponent<Collider2D>().enabled = false;
            GetComponent<SpriteRenderer>().enabled = false;

        }
    }

    #endregion

    #region Movement behavior

    /// <summary>
    /// Compute the direction vector of the enemy depending of the current situation
    /// Here a default null vector is computed as this function shall be overriden by each kind of enemy
    /// </summary>
    override protected Vector2 DirectionComputation()
    {
        Vector2 direction = new Vector2(0, -1) * m_projectileSpeed;
        return direction;
    }

    /// <summary>
    /// Update the position of the enemy
    /// </summary>
    override protected void FixedUpdate()
    {
        // check if an invader has made it to the Dead Zone
        if (gameObject.transform.position.y < -5)
        {
            Destroy(gameObject);
        }

        // move accordingly 
        Vector2 direction = DirectionComputation();
        gameObject.transform.position = (Vector2)gameObject.transform.position + direction * Time.deltaTime;
    }

    #endregion

}
